#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0-preview AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProfanityFilter.Action/ProfanityFilter.Action.csproj", "ProfanityFilter.Action/", "ProfanityFilter.Services/ProfanityFilter.Services.csproj", "ProfanityFilter.Services/"]
RUN dotnet restore "ProfanityFilter.Action/ProfanityFilter.Action.csproj"
COPY . .
WORKDIR "/src/ProfanityFilter.Action"
RUN dotnet build "ProfanityFilter.Action.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
# Install clang/zlib1g-dev dependencies for publishing to native
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    clang zlib1g-dev
RUN dotnet publish "ProfanityFilter.Action.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-preview AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["ProfanityFilter.Action"]