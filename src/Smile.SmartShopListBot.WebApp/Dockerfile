FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Smile.SmartShopListBot.WebApp/Smile.Mediator.Contracts.csproj", "Smile.Mediator.Contracts/"]
COPY ["Smile.SmartShopListBot.WebApp/Smile.Mediator.Core.csproj", "Smile.Mediator.Core/"]
COPY ["Smile.SmartShopListBot.WebApp/Smile.SmartShopListBot.Domain.csproj", "Smile.SmartShopListBot.Domain/"]
COPY ["Smile.SmartShopListBot.WebApp/Smile.SmartShopListBot.Application.csproj", "Smile.SmartShopListBot.Application/"]
COPY ["Smile.SmartShopListBot.WebApp/Smile.SmartShopListBot.Infrastructure.csproj", "Smile.SmartShopListBot.Infrastructure/"]
COPY ["Smile.SmartShopListBot.WebApp/Smile.SmartShopListBot.WebApp.csproj", "Smile.SmartShopListBot.WebApp/"]
RUN dotnet restore "./Smile.SmartShopListBot.WebApp/Smile.SmartShopListBot.WebApp.csproj"
COPY . .
WORKDIR "/src/Smile.SmartShopListBot.WebApp"
RUN dotnet build "./Smile.SmartShopListBot.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Smile.SmartShopListBot.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Smile.SmartShopListBot.WebApp.dll"]