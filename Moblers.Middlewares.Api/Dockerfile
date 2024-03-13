FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Moblers.Middlewares.Api/Moblers.Middlewares.Api.csproj", "Moblers.Middlewares.Api/"]
RUN dotnet restore "Moblers.Middlewares.Api/Moblers.Middlewares.Api.csproj"
COPY . .
WORKDIR "/src/Moblers.Middlewares.Api"
RUN dotnet build "Moblers.Middlewares.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Moblers.Middlewares.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Moblers.Middlewares.Api.dll"]
