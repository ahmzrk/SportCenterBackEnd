# 1. Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Çözümü kopyala
COPY ["PDFReportService/API/API.csproj", "PDFReportService/API/"]
COPY ["PDFReportService/Infrastructure/Business.csproj", "PDFReportService/Infrastructure/"]
COPY ["PDFReportService/Core/Core.csproj", "PDFReportService/Core/"]
COPY ["PDFReportService/DataAccess/DataAccess.csproj", "PDFReportService/DataAccess/"]
COPY ["PDFReportService/Entitites/Entitites.csproj", "PDFReportService/Entitites/"]

RUN dotnet restore "PDFReportService/API/API.csproj"

# Tüm solution kodlarını kopyala
COPY . .
WORKDIR "/src/PDFReportService/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

# 2. Publish aşaması
FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

# 3. Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "API.dll"]
