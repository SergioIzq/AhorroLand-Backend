# ETAPA 1: BUILD - Compilar la aplicación
# Actualizado a .NET 10.0 SDK
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar los archivos de solución y proyectos
COPY AhorroLand.sln ./
COPY AhorroLand.Api/AhorroLand.Api.csproj AhorroLand.Api/
COPY AhorroLand.Application/AhorroLand.Application.csproj AhorroLand.Application/
COPY AhorroLand.Domain/AhorroLand.Domain.csproj AhorroLand.Domain/
COPY AhorroLand.Infrastructure/AhorroLand.Infrastructure.csproj AhorroLand.Infrastructure/
COPY AhorroLand.Shared.Application/AhorroLand.Shared.Application.csproj AhorroLand.Shared.Application/
COPY AhorroLand.Shared.Domain/AhorroLand.Shared.Domain.csproj AhorroLand.Shared.Domain/
COPY AhorroLand.Middleware/AhorroLand.Middleware.csproj AhorroLand.Middleware/

# 🔥 NUEVO: Añadido proyecto FicheroLog detectado en el .sln
COPY AhorroLand.FicheroLog/AhorroLand.FicheroLog.csproj AhorroLand.FicheroLog/

# Restaurar dependencias
RUN dotnet restore

# Copiar el resto del código fuente
COPY . .

# Compilar y publicar AhorroLand.Api
WORKDIR /src/AhorroLand.Api
RUN dotnet build -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ETAPA 2: RUNTIME - Crear la imagen final
# Actualizado a .NET 10.0 ASP.NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Crear directorio de logs con permisos
RUN mkdir -p /app/logs && chmod 777 /app/logs

# Copiar los archivos publicados desde la etapa de build
COPY --from=publish /app/publish .

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl --fail http://localhost/health || exit 1

# Punto de entrada
ENTRYPOINT ["dotnet", "AhorroLand.Api.dll"]