FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
RUN apk add --no-cache icu-libs

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

COPY src/DonationProcessor.Api/DonationProcessor.Api.csproj src/DonationProcessor.Api/
COPY src/DonationProcessor.Application/DonationProcessor.Application.csproj src/DonationProcessor.Application/
COPY src/DonationProcessor.Domain/DonationProcessor.Domain.csproj src/DonationProcessor.Domain/
COPY src/DonationProcessor.Infrastructure/DonationProcessor.Infrastructure.csproj src/DonationProcessor.Infrastructure/

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet restore src/DonationProcessor.Api/DonationProcessor.Api.csproj

COPY . .

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish src/DonationProcessor.Api/DonationProcessor.Api.csproj -c Release -o /app/publish \
      /p:UseAppHost=false /p:PublishSingleFile=false /p:DebugType=None

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DonationProcessor.Api.dll"]