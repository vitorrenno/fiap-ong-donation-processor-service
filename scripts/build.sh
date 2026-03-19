#!/usr/bin/env bash
set -e

echo "Compilando solução DonationProcessor..."
dotnet build DonationProcessor.sln --configuration Release

echo "Build concluído com sucesso."