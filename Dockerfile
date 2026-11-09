
# ====================================
# BUILD STAGE
# ====================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy project file
COPY ["PROGPOE_Frontend/PROGPOE_Frontend.csproj", "PROGPOE_Frontend/"]

# Restore NuGet packages
RUN dotnet restore "PROGPOE_Frontend/PROGPOE_Frontend.csproj"

# Copy the remaining source files
COPY . .

WORKDIR /src

RUN ls -la /src/PROGPOE_Frontend

# Publish application
RUN dotnet publish "/src/PROGPOE_Frontend/PROGPOE_Frontend.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# ====================================
# RUNTIME STAGE
# ====================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

# Application port
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "PROGPOE_Frontend.dll"]
