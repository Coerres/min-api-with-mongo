# Stage 1: Build und Veröffentlichen der Anwendung
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Kopiere den Projektcode und die NuGet-Konfiguration
COPY *.csproj ./
RUN dotnet restore

# Kopiere den restlichen Projektcode und führe den Build durch
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Ausführen der veröffentlichten Anwendung
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Setze Umgebungsvariable ASPNETCORE_URLS auf http://localhost:5001
ENV ASPNETCORE_URLS=http://+:5001

# Öffne den Port 5001 für HTTP
EXPOSE 5001

# Starte die Anwendung
ENTRYPOINT ["dotnet", "WebApi.dll"]
