using AutoMapper;
using GameHub.Application.Dtos.Usuario;
using GameHub.Application.Interface.Repository;
using GameHub.Application.Interface.Service;
using GameHub.Application.Response;
using GameHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GameHub.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IRefreshTokenRepository _refresRepo;
        private readonly IMapper _mapper;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config,
            IRefreshTokenRepository refresRepo,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
            _refresRepo = refresRepo;
            _mapper = mapper;
        }

        #region Métodos Privados

        private async Task<UsuarioDto> MapearUsuarioDtoAsync(ApplicationUser usuario)
        {
            var roles = await _userManager.GetRolesAsync(usuario);

            return new UsuarioDto
            {
                Id = usuario.Id,
                NombreDesarrollador = usuario.NombreDesarrollador,
                Email = usuario.Email!,
                Rol = roles.FirstOrDefault() ?? "",
                PhoneNumber = usuario.PhoneNumber ?? "",
                Pais = usuario.Pais ?? ""
            };
        }

        private static void ValidarResultado(
            IdentityResult resultado,
            string mensajeError)
        {
            if (!resultado.Succeeded)
            {
                var errores = string.Join(
                    " | ",
                    resultado.Errors.Select(e => e.Description));

                throw new InvalidOperationException(
                    $"{mensajeError}: '{errores}'");
            }
        }

        private string GenerarToken(
            ApplicationUser usuario,
            string rol,
            DateTime expiracion)
        {
            var key = _config["JWT_KEY"]
                ?? throw new Exception("JWT_KEY no configurado");

            var issuer = _config["JWT_ISSUER"]
                ?? throw new Exception("JWT_ISSUER no configurado");

            var audience = _config["JWT_AUDIENCE"]
                ?? throw new Exception("JWT_AUDIENCE no configurado");

            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id),

                new Claim(
                    ClaimTypes.Name,
                    usuario.NombreDesarrollador ?? ""),

                new Claim(
                    ClaimTypes.Email,
                    usuario.Email ?? ""),

                new Claim(ClaimTypes.Role, rol)
            };

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = expiracion,

                Issuer = issuer,

                Audience = audience,

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(
                handler.CreateToken(descriptor));
        }

        private string GenerarRefreshToken()
        {
            return Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));
        }

        #endregion

        public async Task<RespuestaLoginDto> LoginAsync(
            UsuarioLoginDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(
                    nameof(dto),
                    "El campo es requerido.");

            var email = dto.Email.Trim().ToLower();

            var usuario = await _userManager.FindByEmailAsync(email);

            if (usuario == null)
                throw new UnauthorizedAccessException(
                    "Usuario no registrado.");

            var passwordCorrecta =
                await _userManager.CheckPasswordAsync(
                    usuario,
                    dto.Password);

            if (!passwordCorrecta)
                throw new UnauthorizedAccessException(
                    "La contraseña es incorrecta.");

            var rol = (
                await _userManager.GetRolesAsync(usuario))
                .FirstOrDefault() ?? "";

            var expiracion = DateTime.UtcNow.AddMinutes(15);

            var jwt = GenerarToken(
                usuario,
                rol,
                expiracion);

            var refresh = GenerarRefreshToken();

            var refreshEntity = new RefreshToken
            {
                Token = refresh,
                UserId = usuario.Id,
                Expiracion = DateTime.UtcNow.AddDays(7)
            };

            await _refresRepo.GuardarAsync(refreshEntity);

            return new RespuestaLoginDto
            {
                Usuario = await MapearUsuarioDtoAsync(usuario),

                AccessToken = jwt,

                RefreshToken = refresh,

                ExpiraEn = expiracion
            };
        }

        public async Task<RespuestaLoginDto> RefreshTokenAsync(
            string refreshToken)
        {
            var tokenDB =
                await _refresRepo.ObtenerAsync(refreshToken);

            if (tokenDB == null ||
                tokenDB.Expiracion < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException(
                    "Refresh token inválido.");
            }

            var usuario = tokenDB.Usuario;

            var rol = (
                await _userManager.GetRolesAsync(usuario))
                .FirstOrDefault() ?? "";

            var expiracion = DateTime.UtcNow.AddMinutes(15);

            var nuevoJwt = GenerarToken(usuario, rol, expiracion);

            var nuevoRefresh = GenerarRefreshToken();

            tokenDB.Token = nuevoRefresh;

            tokenDB.Expiracion = DateTime.UtcNow.AddDays(7);

            await _refresRepo.ActualizarAsync(tokenDB);

            return new RespuestaLoginDto
            {
                Usuario = await MapearUsuarioDtoAsync(usuario),

                AccessToken = nuevoJwt,

                RefreshToken = nuevoRefresh,

                ExpiraEn = expiracion
            };
        }

        public async Task<UsuarioDto> RegistrarUsuarioAsync(
            UsuarioRegistroDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException( nameof(dto), "El campo es requerido.");

            dto.Email = dto.Email.Trim().ToLower();

            var existeEmail =
                await _userManager.FindByEmailAsync(dto.Email);

            if (existeEmail != null)
            {
                throw new InvalidOperationException(
                    "El email ya se encuentra registrado.");
            }

            var rolExistente =
                await _roleManager.RoleExistsAsync(dto.Rol);

            if (!rolExistente)
            {
                throw new InvalidOperationException(
                    $"El rol '{dto.Rol}' no existe.");
            }

            var usuario =
                _mapper.Map<ApplicationUser>(dto);

            usuario.Email = dto.Email;

            usuario.UserName = dto.Email;

            usuario.EmailConfirmed = true;

            var usuarioCreado =
                await _userManager.CreateAsync( usuario, dto.Password);

            ValidarResultado( usuarioCreado,"Error al registrar el usuario");

            var rolAsignado =
                await _userManager.AddToRoleAsync(usuario,dto.Rol);

            ValidarResultado( rolAsignado,"Error al asignar el rol");

            return await MapearUsuarioDtoAsync(usuario);
        }
    }
}