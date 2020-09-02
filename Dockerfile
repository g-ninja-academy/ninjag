#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Ninja.Api/Ninja.Api.csproj", "Ninja.Api/"]
COPY ["Ninja.Infrastructure/Ninja.Infrastructure.csproj", "Ninja.Infrastructure/"]
COPY ["Ninja.Application/Ninja.Application.csproj", "Ninja.Application/"]
COPY ["Ninja.Domain/Ninja.Domain.csproj", "Ninja.Domain/"]
RUN dotnet restore "Ninja.Api/Ninja.Api.csproj"
COPY . .
WORKDIR "/src/Ninja.Api"
RUN dotnet build "Ninja.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ninja.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ninja.Api.dll"]