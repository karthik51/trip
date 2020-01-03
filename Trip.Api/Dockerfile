FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Trip.Api/Trip.Api.csproj", "Trip.Api/"]
RUN dotnet restore "Trip.Api/Trip.Api.csproj"
COPY . .
WORKDIR "/src/Trip.Api"
RUN dotnet build "Trip.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Trip.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Trip.Api.dll"]
