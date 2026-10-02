FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY API.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish API.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "API.dll"]
