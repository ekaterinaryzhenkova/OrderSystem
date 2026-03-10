FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StoreService/StoreService/StoreService.csproj", "StoreService/StoreService/"]
COPY ["StoreService/StoreService.Data/StoreService.Data.csproj", "StoreService/StoreService.Data/"]
RUN dotnet restore "StoreService/StoreService/StoreService.csproj"
COPY . .
WORKDIR "/src/StoreService/StoreService"
RUN dotnet build "StoreService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "StoreService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StoreService.dll"]
