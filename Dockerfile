# ===============================
# BUILD
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar archivos de proyecto
COPY ["GameHub.Solution.slnx", "./"]
COPY ["GameHub.Api/GameHub.Api.csproj", "GameHub.Api/"]
COPY ["GameHub.Application/GameHub.Application.csproj", "GameHub.Application/"]
COPY ["GameHub.Domain/GameHub.Domain.csproj", "GameHub.Domain/"]
COPY ["GameHub.Infrasctruture/GameHub.Infrastructure.csproj", "GameHub.Infrasctruture/"]

# Restaurar dependencias
RUN dotnet restore "GameHub.Solution.slnx"

# Copiar todo el código y publicar
COPY . .

# Publicar la API
WORKDIR "/src/GameHub.Api"
RUN dotnet publish "GameHub.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ===============================
# RUNTIME
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Librerías necesarias para la conexión a base de datos
RUN apt-get update && apt-get install -y libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*

# Copiar archivos publicados
COPY --from=build /app/publish .

# Render usa variable PORT automáticamente
ENV ASPNETCORE_URLS=http://+:${PORT}
EXPOSE 8080

ENTRYPOINT ["dotnet", "GameHub.Api.dll"]