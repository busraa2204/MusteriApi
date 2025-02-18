FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MusteriAPI.API/MusteriAPI.API.csproj", "MusteriAPI.API/"]
COPY ["MusteriAPI.Application/MusteriAPI.Application.csproj", "MusteriAPI.Application/"]
COPY ["MusteriAPI.Domain/MusteriAPI.Domain.csproj", "MusteriAPI.Domain/"]
COPY ["MusteriAPI.Infrastructure/MusteriAPI.Infrastructure.csproj", "MusteriAPI.Infrastructure/"]
RUN dotnet restore "MusteriAPI.API/MusteriAPI.API.csproj"
COPY . .
WORKDIR "/src/MusteriAPI.API"
RUN dotnet build "MusteriAPI.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusteriAPI.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusteriAPI.API.dll"] 