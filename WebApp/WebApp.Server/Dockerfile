FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebApp/WebApp.Server/WebApp.Server.csproj", "WebApp/WebApp.Server/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
RUN dotnet restore "./WebApp/WebApp.Server/./WebApp.Server.csproj"
COPY . .
WORKDIR "/src/WebApp/WebApp.Server"
RUN dotnet build "./WebApp.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./WebApp.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp.Server.dll"]