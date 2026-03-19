# Donation Processor Service

Serviço responsável pelo **processamento assíncrono de doações**, garantindo consistência, escalabilidade e desacoplamento entre os componentes do sistema.

---

## 🧠 Visão Geral

Este serviço atua como um **worker** responsável por:

- consumir eventos de doação (via mensageria)
- processar doações
- garantir idempotência
- atualizar o valor arrecadado das campanhas

Ele é totalmente **desacoplado da API principal**, operando de forma assíncrona para melhorar a escalabilidade e resiliência do sistema.

---

## 🏗️ Arquitetura

O projeto segue os princípios de **Clean Architecture**, organizado em camadas:

- Domain
- Application
- Infrastructure
- Worker

### 📌 Domain
- `Donation` → representa uma doação
- `CampaignBalance` → total arrecadado

### 📌 Application
- `ProcessDonationCommand`
- `ProcessDonationHandler`
- Interfaces de repositório

### 📌 Infrastructure
- EF Core (InMemory)
- Repositórios
- Configuração de persistência

### 📌 Worker
- Entrada do sistema (fila / background)
- Orquestra execução via MediatR

---

## 🔄 Fluxo de Processamento

<div align="left">

Evento da fila (RabbitMQ)

↓  

DonationReceivedEvent

↓

ProcessDonationCommand

↓

ProcessDonationHandler

↓

Persistência

↓

Atualização do saldo da campanha

</div>

---

## 🛡️ Idempotência

O serviço garante que uma mesma doação não seja processada mais de uma vez:

- verificação por `DonationId`
- evita duplicidade em cenários de retry/mensageria

---

## 🧪 Testes

### ✔ Unitários
- validação das entidades
- regras de negócio

### ✔ Integração
- fluxo completo do handler
- persistência
- atualização de saldo
- idempotência

Rodar:

```
dotnet test
```

🚀 Tecnologias

	•	.NET 8
	•	MediatR
	•	Entity Framework Core
	•	xUnit
	•	FluentAssertions

▶️ Como executar

```
dotnet restore
dotnet build
dotnet run --project src/DonationProcessor.Worker
```