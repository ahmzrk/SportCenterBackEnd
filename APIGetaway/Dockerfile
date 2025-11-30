# AŞAMA 1: Build ortamı
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Proje dosyasını ve bağımlılıkları kopyala
COPY ["APIGetaway.csproj", "./"]

# Bağımlılıkları geri yükle
RUN dotnet restore "APIGetaway.csproj"

# Geri kalan tüm proje dosyalarını kopyala
COPY . .

# Projeyi derle
RUN dotnet build "APIGetaway.csproj" -c Release -o /app/build

# AŞAMA 2: Çalışma ortamı (Final Image)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "APIGetaway.dll"]