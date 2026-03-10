FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OrderService/OrderService/OrderService.csproj", "OrderService/OrderService/"]
COPY ["OrderService/OrderService.Data/OrderService.Data.csproj", "OrderService/OrderService.Data/"]
COPY ["OrderService/OrderService.Business/OrderService.Business.csproj", "OrderService/OrderService.Business/"]
RUN dotnet restore "OrderService/OrderService/OrderService.csproj"
COPY . .
WORKDIR "/src/OrderService/OrderService"
RUN dotnet build "OrderService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OrderService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderService.dll"]
