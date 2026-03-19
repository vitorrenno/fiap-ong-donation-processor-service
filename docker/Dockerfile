# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY DonationProcessor.sln ./
COPY src/DonationProcessor.Worker/DonationProcessor.Worker.csproj src/DonationProcessor.Worker/
COPY src/DonationProcessor.Application/DonationProcessor.Application.csproj src/DonationProcessor.Application/
COPY src/DonationProcessor.Domain/DonationProcessor.Domain.csproj src/DonationProcessor.Domain/
COPY src/DonationProcessor.Infrastructure/DonationProcessor.Infrastructure.csproj src/DonationProcessor.Infrastructure/

RUN dotnet restore DonationProcessor.sln

COPY . .

RUN dotnet publish src/DonationProcessor.Worker/DonationProcessor.Worker.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8081
EXPOSE 8081

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DonationProcessor.Worker.dll"]