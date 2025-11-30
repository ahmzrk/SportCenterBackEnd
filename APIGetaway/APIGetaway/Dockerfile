# 1. Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Çözümü kopyala
COPY ["APIGetaway/APIGetaway/APIGetaway.csproj", "APIGetaway/APIGetaway/"]

RUN dotnet restore "APIGetaway/APIGetaway/APIGetaway.csproj"

# Tüm solution kodlarını kopyala
COPY . .
WORKDIR "/src/APIGetaway/APIGetaway"
RUN dotnet build "APIGetaway.csproj" -c Release -o /app/build

# 2. Publish aşaması
FROM build AS publish
RUN dotnet publish "APIGetaway.csproj" -c Release -o /app/publish

# 3. Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "APIGetaway.dll"]
