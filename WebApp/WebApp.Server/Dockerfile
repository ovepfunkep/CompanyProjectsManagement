FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["WebApp/WebApp.Server/WebApp.Server.csproj", "WebApp.Server/WebApp.Server.csproj"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/DataAccess.csproj"]
RUN dotnet restore "WebApp.Server/WebApp.Server.csproj"

COPY . .

WORKDIR "/src/WebApp/WebApp.Server"
RUN dotnet build "WebApp.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebApp.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp.Server.dll"]
