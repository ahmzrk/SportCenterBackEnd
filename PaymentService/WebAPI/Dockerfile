# 1. Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Çözümü kopyala
COPY ["PaymentService/WebAPI/WebAPI.csproj", "PaymentService/WebAPI/"]
COPY ["PaymentService/Entities/Entities.csproj", "PaymentService/Entities/"]

RUN dotnet restore "PaymentService/WebAPI/WebAPI.csproj"

# Tüm solution kodlarını kopyala
COPY . .
WORKDIR "/src/PaymentService/WebAPI"
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
