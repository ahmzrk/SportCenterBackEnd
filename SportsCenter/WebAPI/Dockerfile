# 1. Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Çözümü kopyala
COPY ["SportsCenter/WebAPI/WebAPI.csproj", "SportsCenter/WebAPI/"]
COPY ["SportsCenter/Business/Business.csproj", "SportsCenter/Business/"]
COPY ["SportsCenter/Core/Core.csproj", "SportsCenter/Core/"]
COPY ["SportsCenter/DataAccess/DataAccess.csproj", "SportsCenter/DataAccess/"]
COPY ["SportsCenter/Entities/Entities.csproj", "SportsCenter/Entities/"]

RUN dotnet restore "SportsCenter/WebAPI/WebAPI.csproj"

# Tüm solution kodlarını kopyala
COPY . .
WORKDIR "/src/SportsCenter/WebAPI"
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

# 2. Publish aşaması
FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish

# 3. Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "WebAPI.dll"]

