#!/usr/bin/env bash
set -e

echo "Restaurando pacotes da solução DonationProcessor..."
dotnet restore DonationProcessor.sln

echo "Restore concluído com sucesso."