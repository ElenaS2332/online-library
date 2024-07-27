FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Online-Library.WEB/Online-Library.WEB.csproj", "Online-Library.WEB/"]
COPY ["Online-Library.Domain/Online-Library.Domain.csproj", "Online-Library.Domain/"]
COPY ["Online-Library.Repository/Online-Library.Repository.csproj", "Online-Library.Repository/"]
COPY ["Online-Library.Service/Online-Library.Service.csproj", "Online-Library.Service/"]
RUN dotnet restore "Online-Library.WEB/Online-Library.WEB.csproj"
COPY . .
WORKDIR "/src/Online-Library.WEB"
RUN dotnet build "Online-Library.WEB.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Online-Library.WEB.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Online-Library.WEB.dll"]
