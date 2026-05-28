# Escopo MVP da API do projeto "CentralTicket"

<img width="2391" height="1421" alt="escopo_centralticket-api drawio" src="https://github.com/user-attachments/assets/f41124f3-cc0c-4c48-acdc-1bc381da5762" />

API REST para venda e gerenciamento de ingressos, desenvolvida como projeto da matéria de Arquitetura e Organização de Softwares.

## Tecnologias

- .NET 10
- ASP.NET Core
- Entity Framework Core
- MySQL (Pomelo)
- JWT Bearer Authentication
- Scalar (documentação de API)

## Arquitetura

O projeto segue os princípios de Domain-Driven Design (DDD), organizado em Bounded Contexts independentes:

- **Auth** — cadastro, login e gerenciamento de tokens JWT
- **Billing** — vendas e emissão de ingressos
- **Profile** — dados de perfil do usuário

## Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [MySQL 8+](https://dev.mysql.com/downloads/)
- [EF Core CLI](https://learn.microsoft.com/ef/core/cli/dotnet)

```bash
dotnet tool install --global dotnet-ef
```

## Configuração

Clone o repositório e acesse a pasta do projeto:

```bash
git clone https://github.com/UInfinitu/centralticket-api.git
cd centralticket-api/CentralTicket
```

Edite o arquivo `appsettings.json` com suas credenciais:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=127.0.0.1;Port=3306;Database=CentralTicketDb;User ID=root;Password=suasenha;"
  },
  "AppSettings": {
    "Token": "sua_chave_secreta_jwt_longa",
    "Issuer": "CentralTicket",
    "Audience": "CentralTicketUsers"
  }
}
```

## Banco de dados

Crie e aplique as migrations de cada contexto:

```bash
# Criar as migrations (apenas na primeira vez)
dotnet ef migrations add Init --context AuthDbContext --output-dir Contexts/Auth/Migrations
dotnet ef migrations add Init --context BillingDbContext --output-dir Contexts/Billing/Migrations
dotnet ef migrations add Init --context ProfileDbContext --output-dir Contexts/Profile/Migrations

# Aplicar ao banco
dotnet ef database update --context AuthDbContext
dotnet ef database update --context BillingDbContext
dotnet ef database update --context ProfileDbContext
```

Se as migrations já existirem no repositório, basta aplicar:

```bash
dotnet ef database update --context AuthDbContext
dotnet ef database update --context BillingDbContext
dotnet ef database update --context ProfileDbContext
```

## Executando

```bash
dotnet run
```

A API estará disponível em `http://localhost:5041`.

A documentação interativa (Scalar) pode ser acessada em:

```
http://localhost:5041/scalar
```
