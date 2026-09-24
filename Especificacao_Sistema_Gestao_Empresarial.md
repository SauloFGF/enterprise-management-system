# Sistema de Gestão Empresarial

## Visão Geral
Projeto para validação de stack React + TypeScript + .NET + Entity Framework Core + PostgreSQL.

### Funcionalidades MVP
- Login
- Controle de Usuários
- Controle de Clientes
- Controle de Produtos
- Controle de Pedidos
- Dashboard

### Diferenciais
- JWT
- Refresh Token
- Paginação
- Filtros
- Soft Delete
- RBAC (Admin e Operator)

---

# Descoberta

## Requisitos Confirmados
- Autenticação
- Usuários
- Clientes PF/PJ
- Produtos sem estoque no MVP
- Pedidos
- Dashboard com cards
- PostgreSQL
- React + TypeScript
- .NET + EF Core

## Decisões Aprovadas
- Perfis: ADMIN e OPERATOR
- Clientes: PF e PJ
- Produtos sem estoque
- Pedidos: Pendente, Concluído e Cancelado
- Dashboard apenas com indicadores
- Exclusão lógica (Soft Delete)

---

# Arquitetura

## Arquitetura Geral

Frontend (React)
-> API (.NET)
-> PostgreSQL

## Backend

```text
Api
Application
Domain
Infrastructure
Shared
```

## Frontend

```text
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
├── assets
└── theme
```

---

# Domínio

## Entidades

### User
- Id
- Name
- Email
- PasswordHash
- Role
- RefreshToken
- RefreshTokenExpiresAt
- IsDeleted
- CreatedAt
- UpdatedAt

### Client
- Id
- Type
- Document
- Name
- Email
- Phone

### Product
- Id
- Name
- Description
- Price

### Order
- Id
- ClientId
- Status
- TotalAmount

### OrderItem
- Id
- OrderId
- ProductId
- Quantity
- UnitPrice
- TotalPrice

## Relacionamentos

```text
Client
 └─< Order
        └─< OrderItem >─ Product
```

---

# Autenticação

## JWT
- Expiração: 15 minutos
- Claims: sub, email, role

## Refresh Token
- 7 dias
- Persistido em banco
- Revogável

## RBAC

### ADMIN
- Usuários
- Clientes
- Produtos
- Pedidos
- Dashboard

### OPERATOR
- Clientes
- Produtos
- Pedidos
- Dashboard

---

# APIs

## Auth
POST /api/auth/login
POST /api/auth/refresh
POST /api/auth/logout

## Users
GET /api/users
GET /api/users/{id}
POST /api/users
PUT /api/users/{id}
DELETE /api/users/{id}

## Clients
GET /api/clients
GET /api/clients/{id}
POST /api/clients
PUT /api/clients/{id}
DELETE /api/clients/{id}

## Products
GET /api/products
GET /api/products/{id}
POST /api/products
PUT /api/products/{id}
DELETE /api/products/{id}

## Orders
GET /api/orders
GET /api/orders/{id}
POST /api/orders
PUT /api/orders/{id}
PATCH /api/orders/{id}/status

## Dashboard
GET /api/dashboard

---

# Planejamento

## Fases
1. Foundation
2. Segurança e Autenticação
3. Usuários
4. Clientes
5. Produtos
6. Pedidos
7. Dashboard
8. Qualidade e Release

## Caminho Crítico

```text
Foundation
→ Auth
→ RBAC
→ Clientes
→ Produtos
→ Pedidos
→ Dashboard
→ Testes
→ Release
```

---

# Estrutura Backend

```text
backend/
├── EnterpriseManagement.sln
└── src/
    ├── EnterpriseManagement.Api
    ├── EnterpriseManagement.Application
    ├── EnterpriseManagement.Domain
    ├── EnterpriseManagement.Infrastructure
    └── EnterpriseManagement.Shared
```

# Estrutura Frontend

```text
frontend/
└── src/
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
```

---

# Segurança

- BCrypt para senhas
- JWT assinado
- HTTPS
- CORS configurado
- Soft Delete
- Rate Limiting
- Headers de segurança

---

# Estratégia de Testes

## Backend
- xUnit
- Unitários
- Integração

## Frontend
- Vitest
- React Testing Library

## E2E
- Playwright

Fluxos:
- Login
- Usuários
- Clientes
- Produtos
- Pedidos
- Dashboard

---

# Critérios Globais de Conclusão

- Build sem erros
- Testes aprovados
- Lint aprovado
- Documentação atualizada
- Evidências registradas
- APIs documentadas
- Segurança validada
- Release publicada
