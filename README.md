# Enterprise Management System

Sistema de Gestão Empresarial.

## Tecnologias

### Frontend

- React
- TypeScript
- Vite

Estrutura inicial:

src/

├── app
├── routes
├── pages
├── components
├── layouts
├── services
├── hooks
├── contexts
├── types
├── utils
├── validations
└── assets

O frontend será uma SPA responsável por:
- Login
- Dashboard
- Usuários
- Clientes
- Produtos
- Pedidos

### Backend

- .NET 10
- Entity Framework Core
- PostgreSQL

### Banco

- PostgreSQL

## Funcionalidades

- Login
- Usuários
- Clientes
- Produtos
- Pedidos
- Dashboard

## Setup

### Backend

```bash
# Restore packages
dotnet restore EnterpriseManagement.slnx

# Build
dotnet build EnterpriseManagement.slnx
```

See `docs/backend-configuration.md` for detailed environment configuration, JWT, CORS, database, and migrations setup.

### Frontend

```bash
cd frontend
npm install
npm run dev
```

## Documentation

- [Backend Configuration](docs/backend-configuration.md)
- [Development Backlog](docs/DEVELOPMENT_BACKLOG.md)
- [Architecture](docs/architecture.md)
- [Decisions](docs/decisions/README.md)