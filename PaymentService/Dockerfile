FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebAPI.csproj", "./"]
RUN dotnet restore "WebAPI.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "WebAPI.dll"]