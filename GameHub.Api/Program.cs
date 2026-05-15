using CloudinaryDotNet;
using Ecommerce.Application.Interface.Repository;
using GameHub.Api.Middleware;
using GameHub.Application.Interface.Repository;
using GameHub.Application.Interface.Service;
using GameHub.Application.Mapping;
using GameHub.Application.Service;
using GameHub.Domain.Entities;
using GameHub.Infrastructure.Data;
using GameHub.Infrastructure.Repository;
using GameHub.Infrastructure.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Security.Claims;
using System.Text;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

// Cargar variables de entorno desde el archivo .env
DotNetEnv.Env.Load();
builder.Configuration.AddEnvironmentVariables();

// Leer variables de entorno para la cadena de conexión
var host = Environment.GetEnvironmentVariable("HOST");
var port = Environment.GetEnvironmentVariable("PORT");
var database = Environment.GetEnvironmentVariable("DATABASE");
var user = Environment.GetEnvironmentVariable("USER");
var password = Environment.GetEnvironmentVariable("PASSWORD");
var key = Environment.GetEnvironmentVariable("JWT_KEY");
var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

// Construir la cadena de conexión
var connectionString =
    $"Host={host};" +
    $"Port={port};" +
    $"Database={database};" +
    $"Username={user};" +
    $"Password={password};"+
    $"SSL Mode=Require;" +
    $"Trust Server Certificate=true;";

// Registrar el contexto con la cadena de conexión
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

//Obtener credenciales de Cloudinary desde variables de entorno
var cloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME");
var apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
var apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");

// Crear cuenta de Cloudinary con las credenciales obtenidas
var accont = new Account(cloudName, apiKey, apiSecret);
var cloudinary = new Cloudinary(accont) { Api = { Secure = true } };

// Registrar Cloudinary como un servicio singleton para su uso en toda la aplicación
builder.Services.AddSingleton(cloudinary);

// Definir las reglas de seguridad
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Registrar Repositorios y Interfaces
builder.Services.AddScoped<IVideojuegoRepository, VideojuegoRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IPlataformaRepository, PlataformaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();


//Registrar Servicios y Interfaces
builder.Services.AddScoped<IImageStorageService, CloudinaryImageStorageService>();
builder.Services.AddScoped<IVideojuegoService, VideojuegoService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IPlataformaService, PlataformaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Configurar la autenticación
builder.Services.AddAuthentication
    (
        options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    ).AddJwtBearer(options =>
    {
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            RoleClaimType = ClaimTypes.Role,
            ValidIssuer = issuer,
            ValidAudience = audience
        };

        options.Events = new JwtBearerEvents
        {
            OnChallenge = async context =>
            {
                context.HandleResponse();

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    status = 401,
                    detail = "No autenticado. El token es inválido o no fue enviado."
                }));
            },

            OnForbidden = async context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    status = 403,
                    detail = "Acceso denegado. No tiene permisos para acceder a este recurso."
                }));
            }
        };
    });


//Registrar AutoMapper
builder.Services.AddAutoMapper(cgf => { }, typeof(MappingProfile).Assembly);

builder.Services.AddControllers();


// Swagger / OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "GameHub API",
        Description = """
        #### **Infraestructura escalable para la gestión de videojuegos digitales.**

        GameHub API proporciona un ecosistema robusto para la administración de videojuegos, desarrolladores, géneros y plataformas, integrando autenticación segura, control de acceso y almacenamiento multimedia en la nube.

        ---

        #### Módulos del Sistema
        * **Videojuegos:** Gestión completa de catálogo, imágenes, precios y fechas de lanzamiento.
        * **Usuarios y Roles:** Administración de usuarios mediante autenticación JWT y control de acceso basado en roles.
        * **Géneros y Plataformas:** Organización dinámica del catálogo para facilitar búsquedas y clasificación.
        * **Cloud Storage:** Integración con Cloudinary para almacenamiento y gestión de imágenes.

        #### Características Técnicas
        * **Seguridad:** Autenticación y autorización mediante **JWT + ASP.NET Identity**.
        * **Arquitectura:** Implementación de arquitectura en capas con patrón Repository y Services.
        * **Escalabilidad:** Integración con PostgreSQL, Docker y despliegue en Render.
        * **Documentación:** API documentada y testeable mediante Swagger/OpenAPI.

        ---

        """,

        Contact = new OpenApiContact
        {
            Name = "Ileana - GameHub Backend Developer",
            Email = "ileanag300@gmail.com",
            Url = new Uri("https://github.com/TU_GITHUB")
        },

        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Configuración de seguridad para Swagger (JWT)

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT. Ejemplo: eyJhbGciOiJIUzI1NiIsInR5..."
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference(referenceId: "Bearer", hostDocument: document),
            new List<string>()
        }
    });
});



// Configuración de CORS
var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            policy.WithOrigins(
                "http://localhost:4200",    // Angular
                "http://localhost:3000"    // React
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
        }
        else
        {
            // Solo para desarrollo si no hay configuración
            policy.AllowAnyHeader()
                .AllowAnyMethod();
        }
    });
});


builder.Services.AddOpenApi();

// Construir la aplicación
var app = builder.Build();


// Registrar el middleware de manejo de excepciones
app.UseMiddleware<ExceptionMiddleware>();




// Configuración para entornos de desarrollo y producción

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "GameHub API v1");
});


// Redireccionar la raíz hacia Swagger
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");

    return Task.CompletedTask;
});


// Middleware de CORS
app.UseCors("FrontendPolicy");


// Soporte para autenticación
app.UseAuthentication();

app.UseAuthorization();


// Mapear controladores
app.MapControllers();


// Configuración de ejecución
if (app.Environment.IsDevelopment())
{
    app.Run();
}
else
{
    var apiPort = Environment.GetEnvironmentVariable("PORT") ?? "8080";

    app.Run($"http://0.0.0.0:{apiPort}");
}