FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CustomerAPI.API/CustomerAPI.API.csproj", "CustomerAPI.API/"]
COPY ["CustomerAPI.Application/CustomerAPI.Application.csproj", "CustomerAPI.Application/"]
COPY ["CustomerAPI.Domain/CustomerAPI.Domain.csproj", "CustomerAPI.Domain/"]
COPY ["CustomerAPI.Infrastructure/CustomerAPI.Infrastructure.csproj", "CustomerAPI.Infrastructure/"]
RUN dotnet restore "CustomerAPI.API/CustomerAPI.API.csproj"
COPY . .
WORKDIR "/src/CustomerAPI.API"
RUN dotnet build "CustomerAPI.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerAPI.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerAPI.API.dll"] 