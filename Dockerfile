# Use the SDK image to build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./api/FinancialControllerServer.csproj", "."]
RUN dotnet restore "FinancialControllerServer.csproj"
COPY . .
RUN dotnet build "FinancialControllerServer.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "FinancialControllerServer.csproj" -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinancialControllerServer.dll"]
