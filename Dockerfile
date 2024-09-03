FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ENV ASPNETCORE_ENVIRONMENT=Development
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["verbum-service-web-api/verbum-service-web-api.csproj","verbum-service-web-api/"]
COPY ["verbum-service-infrastructure/verbum-service-infrastructure.csproj","verbum-service-infrastructure/"]
COPY ["verbum-service-application/verbum-service-application.csproj","verbum-service-application/"]
COPY ["verbum-service-domain/verbum-service-domain.csproj","verbum-service-domain/"]
RUN dotnet restore "verbum-service-web-api/verbum-service-web-api.csproj"
COPY . .
WORKDIR /src/verbum-service-web-api
RUN dotnet build "verbum-service-web-api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish 
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "verbum-service-web-api.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8000
ENTRYPOINT [ "dotnet", "verbum-service-web-api.dll", "--environment=Development"]