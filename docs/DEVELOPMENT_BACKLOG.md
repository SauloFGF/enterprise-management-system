# Engineering Backlog — Sistema de Gestão Empresarial

> **Comando executado:** `/backlog`  
> **Data do assessment:** 24/09/2026  
> **Fonte de verdade:** `Especificacao_Sistema_Gestao_Empresarial.md`  
> **Modo:** planejamento; nenhuma implementação de código foi realizada.

## 1. Product Context

O Sistema de Gestão Empresarial é um projeto de portfolio para simular o desenvolvimento de um produto real com um único desenvolvedor. O MVP confirmado pela especificação contém:

- Login;
- Controle de usuários;
- Controle de clientes PF/PJ;
- Controle de produtos sem estoque;
- Controle de pedidos;
- Dashboard com indicadores.

A stack definida é React + TypeScript no frontend e .NET + Entity Framework Core + PostgreSQL no backend. A arquitetura de backend deve preservar os projetos `Api`, `Application`, `Domain`, `Infrastructure` e `Shared`. O frontend deve evoluir dentro da estrutura definida na especificação.

### Guardrails

- A especificação é a fonte de verdade funcional e arquitetural.
- Future information not defined in the specification is recorded as `DECISION_REQUIRED`.
- Suggestions are not requirements until approved and documented.
- Existing implementation is not recreated as a new task.
- Partial implementation receives only the tasks needed to complete or validate it.
- A task is not complete without evidence and verification.

## 2. Current Repository Assessment

### 2.1 Repository inventory

| Área                                     | Evidência atual                                                             | Estado                                            |
| ---------------------------------------- | --------------------------------------------------------------------------- | ------------------------------------------------- |
| Backend                                  | `backend/EnterpriseManagement.slnx` e cinco projetos `.csproj`              | Estrutura existente; sem implementação de negócio |
| API                                      | `Program.cs` com OpenAPI, HTTPS redirection e `/weatherforecast`            | Host de template; nenhum endpoint do MVP          |
| Domain/Application/Infrastructure/Shared | Apenas manifests de projeto                                                 | Sem entidades, regras, DbContext ou adapters      |
| Banco                                    | Nenhum pacote EF Core/Npgsql, connection string, migration ou seed          | Não iniciado                                      |
| Frontend                                 | Vite + React + TypeScript; `App.tsx` renderiza somente um título            | Shell parcial                                     |
| Rotas/UI                                 | `src/routes/index.tsx` retorna `null`; não há pages, layouts ou componentes | Não iniciado                                      |
| API client                               | `src/services/http.ts` está vazio                                           | Não iniciado                                      |
| Autenticação                             | Nenhum JWT, BCrypt, refresh token ou middleware                             | Não iniciado                                      |
| Autorização                              | Nenhuma policy, role guard ou endpoint protegido                            | Não iniciado                                      |
| Testes                                   | Nenhum xUnit, integração, Vitest, RTL ou Playwright                         | Não iniciado                                      |
| CI/CD                                    | `.github` é um arquivo placeholder, não um diretório de workflows           | Não iniciado                                      |
| Documentação                             | README e architecture genéricos; discovery/planning vazios                  | Parcial                                           |
| Decisões                                 | `docs/decisions/README.md` e DEC-001 a DEC-024                              | Aprovadas                                         |
| Ambiente                                 | `appsettings.json` sem banco/JWT; sem `.env.example`                        | Parcial                                           |

### 2.2 Baseline validation

Comandos executados durante o assessment, sem alteração de dados:

| Comando                                                                           | Resultado                   | Interpretação                                            |
| --------------------------------------------------------------------------------- | --------------------------- | -------------------------------------------------------- |
| `dotnet build EnterpriseManagement.slnx --no-restore`                             | PASS com 1 warning          | Build do scaffold; não comprova funcionalidades          |
| `dotnet test EnterpriseManagement.slnx --no-restore`                              | Exit 0, sem saída de testes | Não existem projetos de teste; não é aprovação de testes |
| `npm run lint`                                                                    | PASS                        | Lint do scaffold não falhou                              |
| `npm run build`                                                                   | PASS com warning            | Vite avisou sobre `__dirname` no config                  |
| `npm audit --audit-level=high`                                                    | FAIL                        | Finding alto em `fast-uri` transitivo                    |
| `dotnet list EnterpriseManagement.slnx package --vulnerable --include-transitive` | FAIL/warning                | Finding alto em `Microsoft.OpenApi 2.0.0`                |

O build e o lint do frontend não representam aceite de produto. O backend compila porque o template não contém ainda as funcionalidades do MVP.

**Validação adicional do documento:** `npx prettier --check ..\docs\DEVELOPMENT_BACKLOG.md ..\docs\decisions\*.md` foi executado após a geração e após a aprovação das decisões; todos os arquivos foram considerados formatados pelo Prettier. Não existe script Markdown no `package.json`; esta validação cobre apenas formatação do documento.

### 2.3 Divergences and risks found

1. A especificação menciona `EnterpriseManagement.sln`; o repositório usa `EnterpriseManagement.slnx`.
2. O repositório contém a estrutura de pastas, mas não a estrutura funcional do frontend.
3. O backend contém apenas o endpoint de template `/weatherforecast`.
4. O soft delete é aprovado, mas apenas `User` lista `IsDeleted`; o escopo para as demais entidades precisa ser decidido.
5. A especificação não define DTOs, envelope de erro, paginação, filtros, precisão monetária ou transições de pedido.
6. Não há estratégia de teste, banco de teste, CI/CD ou destino de release.
7. Não há `global.json`, `.nvmrc` ou `engines` para fixar o toolchain.
8. O frontend usa metadados de template (`react-ts`, `0.0.0`) e não declara `strict: true` no `tsconfig.app.json`.
9. `eslint-config-prettier` está instalado, mas não está integrado explicitamente à configuração.
10. Existem findings de dependências que precisam de tratamento ou decisão formal.
11. As lacunas de especificação foram registradas e aprovadas em `docs/decisions/`; nenhuma decisão está bloqueando TASK-002.

## 3. Specification vs Current Implementation

| Área                 | Especificação                                     | Estado atual                                  | Gap                   | Trabalho necessário                                                                       |
| -------------------- | ------------------------------------------------- | --------------------------------------------- | --------------------- | ----------------------------------------------------------------------------------------- |
| Stack                | React, TypeScript, .NET, EF Core, PostgreSQL      | Dependências e scaffold existem; EF/banco não | PARTIALLY_IMPLEMENTED | TASK-002, TASK-003, TASK-012, TASK-013                                                    |
| Arquitetura backend  | Cinco projetos com responsabilidades              | Projetos existem, mas estão vazios            | PARTIALLY_IMPLEMENTED | TASK-006 a TASK-011                                                                       |
| Arquitetura frontend | Estrutura de pastas e SPA                         | Apenas `App`, `routes` vazio e `http` vazio   | PARTIALLY_IMPLEMENTED | TASK-012, TASK-013                                                                        |
| Usuários             | CRUD                                              | Nenhum código ou tela                         | NOT_STARTED           | TASK-026 a TASK-030                                                                       |
| Clientes PF/PJ       | CRUD, filtros e paginação                         | Nenhum código ou tela                         | NOT_STARTED           | TASK-031 a TASK-035                                                                       |
| Produtos             | CRUD sem estoque                                  | Nenhum código ou tela                         | NOT_STARTED           | TASK-036 a TASK-040                                                                       |
| Pedidos              | Itens, total e três status                        | Nenhum código ou tela                         | NOT_STARTED           | TASK-041 a TASK-047                                                                       |
| Dashboard            | Cards/indicadores                                 | Nenhum endpoint ou tela                       | NOT_STARTED           | TASK-048 a TASK-051                                                                       |
| JWT                  | 15 minutos; claims `sub`, `email`, `role`         | Ausente                                       | NOT_STARTED           | TASK-017, TASK-022, TASK-025                                                              |
| Refresh token        | Sete dias, persistido e revogável                 | Ausente                                       | NOT_STARTED           | TASK-019 a TASK-021, TASK-025                                                             |
| RBAC                 | ADMIN e OPERATOR com matriz definida              | Ausente                                       | NOT_STARTED           | TASK-022, TASK-028, TASK-033, TASK-038, TASK-045, TASK-049                                |
| Paginação/filtros    | Diferenciais do produto                           | Contratos ausentes                            | NOT_STARTED           | TASK-004 e tasks de consulta/UI                                                           |
| Soft delete          | Exclusão lógica                                   | Nenhum filtro ou delete behavior              | NOT_STARTED           | DEC-004, TASK-010, TASK-011                                                               |
| Segurança            | BCrypt, JWT, HTTPS, CORS, rate limit, headers     | Apenas HTTPS redirection parcial              | PARTIALLY_IMPLEMENTED | TASK-005, TASK-016 a TASK-023, TASK-055                                                   |
| Testes               | xUnit, integração, Vitest, RTL, Playwright        | Ausentes                                      | NOT_STARTED           | TASK-014, TASK-015, TASK-025, TASK-030, TASK-035, TASK-040, TASK-047, TASK-051 a TASK-054 |
| Documentação         | Setup, API, arquitetura, decisões e release       | Incompleta                                    | PARTIALLY_IMPLEMENTED | TASK-056, TASK-057, TASK-059                                                              |
| CI/CD                | Critérios globais de build/testes/lint            | Nenhum workflow                               | NOT_STARTED           | DEC-021, TASK-059                                                                         |
| Release              | Build, testes, lint, segurança, docs e publicação | Não pronto                                    | NOT_STARTED           | TASK-055, TASK-059, TASK-060                                                              |

## 4. Product Scope and Non-Goals

### In scope

- Login e sessão com JWT e refresh token.
- Administração de usuários para ADMIN.
- Clientes PF/PJ para ADMIN e OPERATOR.
- Produtos sem estoque para ADMIN e OPERATOR.
- Pedidos com itens, total e status Pendente, Concluído e Cancelado.
- Dashboard com indicadores/cards.
- Paginação, filtros, soft delete, validação, tratamento de erros e API documentada.
- Testes unitários, integração, componentes e E2E dos fluxos definidos.

### Out of scope or not assumed

- Controle de estoque, explicitamente fora do MVP.
- Pagamentos, frete, emissão fiscal, BI e integrações não mencionados na especificação.
- Novos roles, novos status ou permissões por ação sem decisão.
- Exclusão física quando o fluxo aprovado for soft delete.
- Docker, provider de cloud e plataforma de release sem decisão.

## 5. Architecture Guardrails

### Backend

```text
Api → Application → Domain
          ↑             ↑
          └──── Infrastructure

Shared deve conter apenas contratos realmente compartilhados.
```

- Regras de negócio pertencem a Domain/Application.
- EF Core, Npgsql, BCrypt e JWT pertencem a Infrastructure.
- HTTP, middleware, policies e composition root pertencem a Api.
- O estilo de endpoint é `DEC-019` (approved); o template atual usa minimal APIs.

### Frontend

- `routes`, `pages`, `layouts`, `components`, `services`, `hooks`, `contexts`, `types`, `utils`, `validations` e `theme` devem receber responsabilidades separadas.
- A API é a fonte de autorização; esconder botão no frontend não substitui policy no backend.
- O caminho das rotas e o armazenamento de tokens seguem DEC-018 e DEC-013 aprovadas.

## 6. Epics

| Epic     | Nome                      | Estado  | Resultado esperado                                               |
| -------- | ------------------------- | ------- | ---------------------------------------------------------------- |
| EPIC-001 | Foundation                | READY   | Base reproduzível, configuração, domínio, banco e frontend shell |
| EPIC-002 | Authentication & Security | BLOCKED | Login, JWT, refresh/logout, RBAC e controles de segurança        |
| EPIC-003 | User Management           | BLOCKED | CRUD de usuários para ADMIN                                      |
| EPIC-004 | Client Management         | BLOCKED | CRUD de clientes PF/PJ                                           |
| EPIC-005 | Product Management        | BLOCKED | CRUD de produtos sem estoque                                     |
| EPIC-006 | Order Management          | BLOCKED | Pedidos transacionais, totais e status                           |
| EPIC-007 | Dashboard                 | BLOCKED | Endpoint e cards de indicadores                                  |
| EPIC-008 | Testing & Quality         | BLOCKED | xUnit, integração, RTL, Playwright e auditoria                   |
| EPIC-009 | Documentation & Release   | BLOCKED | Documentação, CI/CD e release reproduzível                       |

## 7. Features

### EPIC-001 — Foundation

- **FEAT-001.1 — Decision governance:** registrar decisões pendentes e dependências. Sem implementação de produto.
- **FEAT-001.2 — Backend configuration:** packages, environment e configuração de banco/JWT.
- **FEAT-001.3 — Domain and persistence:** entidades, relacionamentos, EF Core, migration e seed aprovado.
- **FEAT-001.4 — Cross-cutting API:** erros, validação, paginação, filtros e pipeline HTTP.
- **FEAT-001.5 — SPA foundation:** providers, router, layouts, tema e API client.
- **FEAT-001.6 — Test foundation:** projetos e comandos de teste backend, frontend e E2E.

### EPIC-002 — Authentication & Security

- **FEAT-002.1 — Password and login:** BCrypt, credenciais e `POST /api/auth/login`.
- **FEAT-002.2 — JWT:** emissão, claims, assinatura, expiração e validação.
- **FEAT-002.3 — Refresh and logout:** persistência, sete dias, revogação e endpoints.
- **FEAT-002.4 — RBAC:** matriz ADMIN/OPERATOR aplicada no backend.
- **FEAT-002.5 — Frontend session:** contexto, login, renewal, logout e protected routes.
- **FEAT-002.6 — Security controls:** HTTPS, CORS, rate limiting, headers e secrets.

### EPIC-003 — User Management

- **FEAT-003.1 — User API:** domain, DTOs, service, CRUD e soft delete.
- **FEAT-003.2 — User UI:** listagem, filtros, paginação, formulário e detalhe.
- **FEAT-003.3 — User quality:** testes de domínio, API, autorização e UI.

### EPIC-004 — Client Management

- **FEAT-004.1 — Client API:** PF/PJ, documentos, filtros e CRUD.
- **FEAT-004.2 — Client UI:** listagem, formulário condicional e detalhe.
- **FEAT-004.3 — Client quality:** validação, duplicidade, soft delete, RBAC e testes.

### EPIC-005 — Product Management

- **FEAT-005.1 — Product API:** produtos sem estoque, preço e CRUD.
- **FEAT-005.2 — Product UI:** catálogo, filtros e formulário.
- **FEAT-005.3 — Product quality:** preço, delete behavior, RBAC e testes.

### EPIC-006 — Order Management

- **FEAT-006.1 — Order domain:** Order, OrderItem, total, transação e status.
- **FEAT-006.2 — Order API:** listagem, detalhe, create, update e status patch.
- **FEAT-006.3 — Order UI:** consulta, criação e mudança de status.
- **FEAT-006.4 — Order quality:** rollback, total, transições, concorrência e testes.

### EPIC-007 — Dashboard

- **FEAT-007.1 — Dashboard API:** métricas, query e endpoint protegido.
- **FEAT-007.2 — Dashboard UI:** cards e estados de carregamento/erro/vazio.
- **FEAT-007.3 — Dashboard quality:** cálculos, autorização e testes.

### EPIC-008 — Testing & Quality

- **FEAT-008.1 — Backend tests:** xUnit, integração e banco de teste.
- **FEAT-008.2 — Frontend tests:** Vitest e React Testing Library.
- **FEAT-008.3 — E2E tests:** Playwright para os seis fluxos da especificação.
- **FEAT-008.4 — Quality/security:** lint, build, auditoria e evidências.

### EPIC-009 — Documentation & Release

- **FEAT-009.1 — Documentation:** README, setup, API, arquitetura e decisões.
- **FEAT-009.2 — CI/CD:** restore, build, lint, testes e promotion gates.
- **FEAT-009.3 — Release:** smoke tests, migrations, rollback e checklist.

## 8. Task Index and Current Queue

| ID       | Task                                              | Status  | Size | Depends on                                                                     |
| -------- | ------------------------------------------------- | ------- | ---- | ------------------------------------------------------------------------------ |
| TASK-001 | Consolidar baseline e decisões bloqueadoras       | DONE    | S    | —                                                                              |
| TASK-002 | Configurar packages e ambiente do backend         | READY   | M    | TASK-001, DEC-011, DEC-014, DEC-015, DEC-016, DEC-022, DEC-023                 |
| TASK-003 | Registrar EF Core e Npgsql no backend             | BLOCKED | S    | TASK-002, DEC-019, DEC-022, DEC-023                                            |
| TASK-004 | Definir contratos comuns de API                   | READY   | M    | TASK-001, DEC-001, DEC-002, DEC-003                                            |
| TASK-005 | Configurar pipeline HTTP e segurança transversal  | BLOCKED | M    | TASK-002, TASK-003, DEC-001, DEC-014, DEC-015, DEC-016, DEC-024                |
| TASK-006 | Criar entidade User e roles                       | READY   | S    | TASK-001, DEC-003, DEC-004, DEC-010                                            |
| TASK-007 | Criar entidade Client PF/PJ                       | READY   | S    | TASK-001, DEC-003, DEC-004, DEC-006                                            |
| TASK-008 | Criar entidade Product sem estoque                | READY   | S    | TASK-001, DEC-003, DEC-004, DEC-007                                            |
| TASK-009 | Criar entidades Order e OrderItem                 | READY   | M    | TASK-001, DEC-003, DEC-004, DEC-008, DEC-009                                   |
| TASK-010 | Configurar DbContext e relacionamentos            | BLOCKED | M    | TASK-003, TASK-006 a TASK-009, DEC-004, DEC-005                                |
| TASK-011 | Criar migration e seed aprovados                  | BLOCKED | M    | TASK-010, DEC-010, DEC-020, DEC-023                                            |
| TASK-012 | Completar shell e providers da SPA                | READY   | M    | TASK-001, DEC-018, DEC-022                                                     |
| TASK-013 | Criar API client e tratamento de erros            | BLOCKED | S    | TASK-004, DEC-001, DEC-013                                                     |
| TASK-014 | Criar infraestrutura de testes backend            | READY   | M    | TASK-001, DEC-020, DEC-021, DEC-022                                            |
| TASK-015 | Criar infraestrutura Vitest/RTL e Playwright      | READY   | M    | TASK-001, DEC-020, DEC-021, DEC-022                                            |
| TASK-016 | Implementar BCrypt password hasher                | BLOCKED | S    | TASK-002, DEC-010                                                              |
| TASK-017 | Configurar serviço JWT e claims                   | BLOCKED | M    | TASK-002, DEC-011                                                              |
| TASK-018 | Implementar login no backend                      | BLOCKED | M    | TASK-004, TASK-010, TASK-016, TASK-017, DEC-003, DEC-010                       |
| TASK-019 | Implementar persistência e ciclo do refresh token | BLOCKED | M    | TASK-006, TASK-010, DEC-012                                                    |
| TASK-020 | Implementar endpoint de refresh                   | BLOCKED | S    | TASK-004, TASK-019                                                             |
| TASK-021 | Implementar logout e revogação                    | BLOCKED | S    | TASK-019, TASK-023, DEC-012                                                    |
| TASK-022 | Configurar middleware JWT e policies RBAC         | BLOCKED | M    | TASK-017, DEC-010, DEC-011                                                     |
| TASK-023 | Criar contexto de autenticação frontend           | BLOCKED | M    | TASK-013, TASK-018, TASK-020, TASK-021, DEC-013                                |
| TASK-024 | Criar login e protected routes                    | BLOCKED | M    | TASK-012, TASK-022, TASK-023, DEC-018                                          |
| TASK-025 | Criar testes de autenticação e autorização        | BLOCKED | M    | TASK-014, TASK-015, TASK-018 a TASK-024                                        |
| TASK-026 | Criar DTOs e validação de User                    | BLOCKED | S    | TASK-004, TASK-006, DEC-003, DEC-010                                           |
| TASK-027 | Implementar consultas de User                     | BLOCKED | M    | TASK-010, TASK-026, DEC-002, DEC-004                                           |
| TASK-028 | Expor CRUD de Users para ADMIN                    | BLOCKED | M    | TASK-022, TASK-026, TASK-027, DEC-001, DEC-004                                 |
| TASK-029 | Criar telas de User Management                    | BLOCKED | M    | TASK-013, TASK-024, TASK-028, DEC-018                                          |
| TASK-030 | Testar User Management                            | BLOCKED | M    | TASK-014, TASK-015, TASK-028, TASK-029                                         |
| TASK-031 | Criar DTOs e validação de Client                  | BLOCKED | S    | TASK-004, TASK-007, DEC-003, DEC-006                                           |
| TASK-032 | Implementar service de Client                     | BLOCKED | M    | TASK-010, TASK-031, DEC-002, DEC-004, DEC-005, DEC-006                         |
| TASK-033 | Expor CRUD de Clients                             | BLOCKED | M    | TASK-022, TASK-031, TASK-032                                                   |
| TASK-034 | Criar telas de Client Management                  | BLOCKED | M    | TASK-013, TASK-033, DEC-018                                                    |
| TASK-035 | Testar Client Management                          | BLOCKED | M    | TASK-014, TASK-015, TASK-033, TASK-034                                         |
| TASK-036 | Criar DTOs e validação de Product                 | BLOCKED | S    | TASK-004, TASK-008, DEC-003, DEC-007                                           |
| TASK-037 | Implementar service de Product                    | BLOCKED | M    | TASK-010, TASK-036, DEC-002, DEC-004, DEC-005, DEC-007                         |
| TASK-038 | Expor CRUD de Products                            | BLOCKED | M    | TASK-022, TASK-036, TASK-037                                                   |
| TASK-039 | Criar telas de Product Management                 | BLOCKED | M    | TASK-013, TASK-038, DEC-018                                                    |
| TASK-040 | Testar Product Management                         | BLOCKED | M    | TASK-014, TASK-015, TASK-038, TASK-039                                         |
| TASK-041 | Definir contratos e regras de Order               | BLOCKED | M    | TASK-001, TASK-009, DEC-003, DEC-007, DEC-008, DEC-009                         |
| TASK-042 | Implementar consultas de Order                    | BLOCKED | M    | TASK-010, TASK-041, DEC-002, DEC-005                                           |
| TASK-043 | Implementar criação transacional e total          | BLOCKED | M    | TASK-041, TASK-042, DEC-007, DEC-009                                           |
| TASK-044 | Implementar transições de status                  | BLOCKED | M    | TASK-041, TASK-042, DEC-008                                                    |
| TASK-045 | Expor APIs de Orders                              | BLOCKED | M    | TASK-022, TASK-041 a TASK-044, DEC-001, DEC-002                                |
| TASK-046 | Criar telas de Order Management                   | BLOCKED | M    | TASK-013, TASK-045, DEC-018                                                    |
| TASK-047 | Testar Order Management                           | BLOCKED | L    | TASK-014, TASK-015, TASK-043 a TASK-046                                        |
| TASK-048 | Definir métricas do Dashboard                     | READY   | S    | TASK-001, DEC-007, DEC-017                                                     |
| TASK-049 | Implementar query e endpoint do Dashboard         | BLOCKED | M    | TASK-022, TASK-042, TASK-048                                                   |
| TASK-050 | Criar tela de Dashboard                           | BLOCKED | M    | TASK-013, TASK-049, DEC-018                                                    |
| TASK-051 | Testar Dashboard                                  | BLOCKED | M    | TASK-014, TASK-015, TASK-049, TASK-050                                         |
| TASK-052 | Consolidar testes backend por camadas             | BLOCKED | M    | TASK-014, TASK-025, TASK-030, TASK-035, TASK-040, TASK-047, TASK-051           |
| TASK-053 | Consolidar testes frontend                        | BLOCKED | M    | TASK-015, TASK-023, TASK-029, TASK-034, TASK-039, TASK-046, TASK-050           |
| TASK-054 | Automatizar E2E dos seis fluxos                   | BLOCKED | L    | TASK-015, TASK-025, TASK-030, TASK-035, TASK-040, TASK-047, TASK-051           |
| TASK-055 | Executar hardening e auditoria                    | BLOCKED | M    | TASK-005, TASK-016 a TASK-025, DEC-014 a DEC-016, DEC-024                      |
| TASK-056 | Atualizar README e setup                          | BLOCKED | M    | TASK-002, TASK-011, DEC-020, DEC-023                                           |
| TASK-057 | Documentar API, arquitetura e decisões            | BLOCKED | M    | TASK-001, TASK-004, TASK-022, TASK-028, TASK-033, TASK-038, TASK-045, TASK-049 |
| TASK-058 | Criar pipeline CI/CD                              | BLOCKED | M    | DEC-021, TASK-014, TASK-015, TASK-052 a TASK-055                               |
| TASK-059 | Executar release checklist e smoke tests          | BLOCKED | M    | TASK-055 a TASK-058, DEC-017, DEC-021, DEC-023                                 |
| TASK-060 | Fechar rastreabilidade e readiness                | BLOCKED | S    | TASK-059                                                                       |

**Current task:** `TASK-002`  
**Next READY task:** `TASK-002`  
**Current epic:** EPIC-001 — Foundation  
**Current feature:** FEAT-001.2 — Backend configuration

# TASK-001 — Consolidar baseline e decisões bloqueadoras

STATUS: DONE

EPIC: EPIC-001

FEATURE: FEAT-001.1

SIZE: S

DEPENDS ON: —

## CONTEXTO

O assessment identificou lacunas que afetam contratos, persistência, segurança e release. A especificação não define todos os detalhes necessários para implementação. Sem um registro explícito, o desenvolvedor poderia introduzir regras silenciosamente e o QA não teria uma referência para verificar a decisão.

## OBJETIVO

Criar um log versionado para `DEC-001` a `DEC-024`, preservando a distinção entre requisito confirmado, decisão pendente, sugestão e melhoria.

## O QUE VOCÊ DEVE FAZER

1. Criar `docs/decisions/`.
2. Criar um registro por decisão com ID, status, data, responsável, contexto, decisão, justificativa, impacto e tasks afetadas.
3. Para futuras lacunas, registrar como `PENDING` qualquer decisão ainda não aprovada.
4. Marcar explicitamente as decisões já confirmadas na especificação.
5. Atualizar as referências do backlog somente para decisões aprovadas.
6. Listar as tasks que permanecem bloqueadas.

## ONDE TRABALHAR

- `docs/decisions/`
- `docs/DEVELOPMENT_BACKLOG.md`

## NÃO FAÇA

- Não implementar endpoints, entidades, migrations ou telas.
- Não alterar `Especificacao_Sistema_Gestao_Empresarial.md`.
- Não escolher uma opção sem aprovação.
- Não transformar sugestões em requisitos.

## REGRAS TÉCNICAS

- Requisitos confirmados da especificação não podem ser contraditos silenciosamente.
- `DECISION_REQUIRED` deve ser usado para toda lacuna relevante.
- Cada decisão deve ter owner, data, status e impacto.
- A decisão deve referenciar a task que libera ou bloqueia.

## CRITÉRIOS DE ACEITE

- [x] Existem registros para DEC-001 a DEC-024.
- [x] Cada registro possui status, responsável, data, impacto e tasks afetadas.
- [x] Nenhuma decisão pendente está apresentada como aprovada.
- [x] Sugestões estão marcadas como não requisito.
- [x] O backlog identifica a próxima task READY.

## TESTES OBRIGATÓRIOS

- [x] Revisão documental de IDs e links.
- [x] Conferência de que todos os DEC referenciados existem.
- [x] Revisão do diff para confirmar que nenhum código de aplicação foi alterado.

## COMO VALIDAR

- Revisão documental e inspeção do diff.
- Não há script de teste específico para esta task.
- Comandos de build não são necessários para validar um documento.

## EVIDÊNCIAS

- `docs/decisions/README.md` e DEC-001 a DEC-024.
- Data e responsável em cada decisão.
- Diff revisável.
- Tasks liberadas e bloqueadas atualizadas.

## DEFINITION OF DONE

- [x] Decisões registradas e revisadas.
- [x] Dependências atualizadas.
- [x] Nenhum arquivo de código de aplicação alterado.
- [x] Próxima task identificada.

## PRÓXIMA TASK

TASK-002 — Configurar packages e ambiente do backend, após a aprovação das decisões relevantes.

# TASK-002 — Configurar packages e ambiente do backend

STATUS: READY

EPIC: EPIC-001

FEATURE: FEAT-001.2

SIZE: M

DEPENDS ON: TASK-001, DEC-011, DEC-014, DEC-015, DEC-016, DEC-022, DEC-023

## CONTEXTO

Os cinco projetos .NET já existem, mas não possuem as packages de EF Core, Npgsql, autenticação, hashing ou configuração externa. A aplicação também não possui uma fonte segura para connection string, chaves JWT e limites de segurança.

## OBJETIVO

Adicionar somente as dependências aprovadas e criar uma configuração de ambiente reproduzível, sem secrets no repositório.

## O QUE VOCÊ DEVE FAZER

1. Registrar as versões de runtime e packages aprovadas.
2. Adicionar packages de EF Core/Npgsql, JWT e BCrypt somente se compatível com a decisão.
3. Criar bindings de configuração para banco, JWT, CORS, rate limit e headers.
4. Criar exemplo de ambiente sem valores sensíveis.
5. Verificar dependências vulneráveis e registrar o resultado.
6. Atualizar documentação de configuração.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Api/EnterpriseManagement.Api.csproj`
- `backend/src/EnterpriseManagement.Infrastructure/EnterpriseManagement.Infrastructure.csproj`
- `backend/src/EnterpriseManagement.Api/appsettings*.json`
- Configuração de ambiente local
- `docs/`

## NÃO FAÇA

- Não criar novamente os cinco projetos.
- Não escolher issuer, audience, algoritmo ou chave sem DEC-011.
- Não commitar senha, connection string real ou signing key.
- Não corrigir dependências com update cego.

## REGRAS TÉCNICAS

- JWT deve ter expiração de 15 minutos e claims `sub`, `email`, `role`.
- Senhas devem usar BCrypt.
- HTTPS, CORS, rate limiting e headers são requisitos da especificação.
- Valores específicos de ambiente seguem DEC-011, DEC-014, DEC-015, DEC-016 e DEC-023 aprovadas.

## CRITÉRIOS DE ACEITE

- [ ] Restore e build continuam funcionando.
- [ ] A configuração pode ser carregada por ambiente.
- [ ] Não existe secret no repositório.
- [ ] Findings de dependência têm status, impacto e decisão.
- [ ] O comando de restore está documentado.

## TESTES OBRIGATÓRIOS

- Teste de configuração de Options para cada binding aprovado.
- Teste de que secrets ausentes produzem erro de configuração seguro, se aplicável.
- Auditoria de packages NuGet e npm quando relacionada ao backend.

## COMO VALIDAR

```text
dotnet restore EnterpriseManagement.slnx
dotnet build EnterpriseManagement.slnx
dotnet list EnterpriseManagement.slnx package --vulnerable --include-transitive
```

## EVIDÊNCIAS

- Diff de manifests.
- Configuração de exemplo.
- Saída de restore/build/auditoria.
- Registro das decisões aplicadas.

## DEFINITION OF DONE

- [ ] Packages e configuração aprovadas.
- [ ] Build passa.
- [ ] Secrets ausentes do repositório.
- [ ] Findings registrados.

## PRÓXIMA TASK

TASK-003 — Registrar EF Core e Npgsql no backend.

# TASK-003 — Registrar EF Core e Npgsql no backend

STATUS: BLOCKED

EPIC: EPIC-001

FEATURE: FEAT-001.2

SIZE: S

DEPENDS ON: TASK-002, DEC-019, DEC-022, DEC-023

## CONTEXTO

O backend possui a solução e os projetos, mas não possui registro de EF Core, Npgsql ou `DbContext`. A camada Infrastructure é o local adequado para a integração de persistência.

## OBJETIVO

Registrar as packages de persistência e o `DbContext` no container de DI, sem criar ainda o modelo completo.

## O QUE VOCÊ DEVE FAZER

1. Adicionar as referências EF Core/Npgsql aprovadas.
2. Criar o `DbContext` no projeto Infrastructure.
3. Registrar o contexto no `Api` usando a connection string de ambiente.
4. Configurar design-time factory se necessária para migrations.
5. Criar teste de registro e resolução do contexto.
6. Não registrar entidades de negócio antes da TASK-006 a TASK-009.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Infrastructure/`
- `backend/src/EnterpriseManagement.Api/Program.cs`
- `.csproj` correspondentes

## NÃO FAÇA

- Não criar tabelas ou migrations nesta task.
- Não adicionar entities de negócio.
- Não usar InMemory ou SQLite como substituto silencioso do PostgreSQL.

## REGRAS TÉCNICAS

- Provider deve ser PostgreSQL conforme a especificação.
- Dependências de Infrastructure não devem entrar em Domain.
- Connection string vem de configuração externa.

## CRITÉRIOS DE ACEITE

- [ ] `DbContext` é resolvido pelo container de DI.
- [ ] Provider e conexão podem ser configurados por ambiente.
- [ ] Build e teste de registro passam.
- [ ] Nenhuma entidade de negócio foi adicionada prematuramente.

## TESTES OBRIGATÓRIOS

- Teste unitário de registro do `DbContext`.
- Teste de configuração inválida sem expor connection string.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Diff de `.csproj` e `Program.cs`.
- Teste de resolução do contexto.
- ConfiguraçãoUsed no log sem segredo.

## DEFINITION OF DONE

- [ ] EF Core/Npgsql registrados.
- [ ] DbContext testado.
- [ ] Nenhuma migration criada.

## PRÓXIMA TASK

TASK-004 — Definir contratos comuns de API.

# TASK-004 — Definir contratos comuns de API

STATUS: READY

EPIC: EPIC-001

FEATURE: FEAT-001.4

SIZE: M

DEPENDS ON: TASK-001, DEC-001, DEC-002, DEC-003

## CONTEXTO

A especificação define rotas, mas não define envelope de erro, status codes completos, formato de paginação, filtros ou DTOs. Implementar endpoints antes disso criaria incompatibilidade entre backend, frontend e testes.

## OBJETIVO

Documentar e implementar os contratos transversais que serão reutilizados por todas as APIs.

## O QUE VOCÊ DEVE FAZER

1. Registrar o formato de erro aprovado.
2. Mapear status codes para validação, autenticação, autorização, conflito e not found.
3. Definir query e response de paginação.
4. Definir filtros e ordenação por recurso.
5. Criar tipos/validadores comuns na camada apropriada.
6. Criar testes de contrato e serialização.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Shared/` ou `Application/`
- `backend/src/EnterpriseManagement.Api/`
- `frontend/src/types/` e `frontend/src/services/`
- Documentação de API

## NÃO FAÇA

- Não implementar regras específicas de User, Client, Product ou Order.
- Não inventar envelope, defaults ou limites.
- Não escolher status codes apenas por conveniência da biblioteca.

## REGRAS TÉCNICAS

- A especificação exige paginação e filtros, mas não define seu formato; usar DEC-002.
- Nenhum secret deve aparecer em error response.
- A API deve documentar requests, responses e erros.

## CRITÉRIOS DE ACEITE

- [ ] Formato de erro está documentado e implementado.
- [ ] Status codes têm semântica aprovada.
- [ ] Paginação e filtros possuem nomes, defaults e limites aprovados.
- [ ] Testes cobrem serialização de sucesso e erro.

## TESTES OBRIGATÓRIOS

- Teste de response de lista paginada.
- Teste de erro de validação.
- Teste de error mapping para 401, 403 e 404.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Contrato de API.
- Testes.
- Exemplo sanitizado de request e response.

## DEFINITION OF DONE

- [ ] DEC-001 a DEC-003 aplicadas.
- [ ] Contrato documentado.
- [ ] Testes de contrato passam.

## PRÓXIMA TASK

TASK-005 — Configurar pipeline HTTP e segurança transversal.

# TASK-005 — Configurar pipeline HTTP e segurança transversal

STATUS: BLOCKED

EPIC: EPIC-001

FEATURE: FEAT-001.4

SIZE: M

DEPENDS ON: TASK-002, TASK-003, DEC-001, DEC-014, DEC-015, DEC-016, DEC-024

## CONTEXTO

`Program.cs` ainda contém o template com `/weatherforecast`. O pipeline não possui CORS, rate limiting, headers de segurança ou exception handling padronizado.

## OBJETIVO

Configurar o host para que endpoints futuros utilizem um pipeline coerente, seguro e testável.

## O QUE VOCÊ DEVE FAZER

1. Registrar o `DbContext` e configurações aprovadas.
2. Configurar HTTPS e, se aprovado, HSTS.
3. Configurar CORS com origens explícitas.
4. Configurar rate limiting conforme DEC-015.
5. Configurar headers de segurança conforme DEC-016.
6. Registrar exception handler e logging sem dados sensíveis.
7. Remover ou substituir o endpoint de template que não pertence ao MVP.
8. Manter OpenAPI conforme a decisão de documentação.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Api/Program.cs`
- `backend/src/EnterpriseManagement.Api/appsettings*.json`
- Middlewares/options do projeto Api

## NÃO FAÇA

- Não implementar endpoints de negócio.
- Não escolher origem CORS ou limite sem decisão.
- Não expor stack trace em produção.

## REGRAS TÉCNICAS

- HTTPS é requisito.
- CORS, rate limiting e headers são requisitos.
- Erros devem seguir TASK-004.
- AllowedHosts deve ter comportamento documentado por ambiente.

## CRITÉRIOS DE ACEITE

- [ ] Pipeline inicia sem erro.
- [ ] CORS rejeita origem não aprovada.
- [ ] Rate limiting está aplicado conforme decisão.
- [ ] Headers são retornados conforme decisão.
- [ ] `/weatherforecast` não é tratado como endpoint do produto.

## TESTES OBRIGATÓRIOS

- Teste de CORS permitido e rejeitado.
- Teste de headers.
- Teste de rate limit.
- Teste de HTTPS/redirection e error response.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Headers capturados.
- Testes de pipeline.
- Configuração por ambiente.

## DEFINITION OF DONE

- [ ] Pipeline seguro configurado.
- [ ] Decisões de segurança aplicadas.
- [ ] Testes relevantes passam.

## PRÓXIMA TASK

TASK-006 — Criar entidade User e roles.

# TASK-006 — Criar entidade User e roles

STATUS: READY

EPIC: EPIC-001

FEATURE: FEAT-001.3

SIZE: S

DEPENDS ON: TASK-001, DEC-003, DEC-004, DEC-010

## CONTEXTO

A especificação define User com `Id`, `Name`, `Email`, `PasswordHash`, `Role`, `RefreshToken`, `RefreshTokenExpiresAt`, `IsDeleted`, `CreatedAt` e `UpdatedAt`. Não define email unique, password policy ou regras do último administrador.

## OBJETIVO

Criar a entidade de domínio e os enums de role sem adicionar campos não aprovados.

## O QUE VOCÊ DEVE FAZER

1. Criar a entidade User em Domain.
2. Criar roles ADMIN e OPERATOR.
3. Mapear apenas os campos confirmados e os que forem aprovados em DEC-003/004/010.
4. Adicionar invariantes de domínio explicitamente aprovadas.
5. Criar testes de construção e role inválida.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Domain/`

## NÃO FAÇA

- Não mapear EF Core nesta task.
- Não criar migration.
- Não inventar unique constraint, password policy ou último admin.
- Não adicionar MFA, OAuth ou recuperação de senha.

## REGRAS TÉCNICAS

- Roles confirmados são ADMIN e OPERATOR.
- `IsDeleted`, `CreatedAt` e `UpdatedAt` existem em User conforme a especificação.
- PasswordHash não é senha plaintext.

## CRITÉRIOS DE ACEITE

- [ ] User contém todos os campos confirmados.
- [ ] Roles são exatamente ADMIN e OPERATOR.
- [ ] Nenhum campo extra sem decisão.
- [ ] Testes unitários de invariants passam.

## TESTES OBRIGATÓRIOS

- Construção válida.
- Role inválida.
- Campos obrigatórios conforme contrato aprovado.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Arquivo de domínio.
- Testes unitários.
- Decisões aplicadas.

## DEFINITION OF DONE

- [ ] Entidade User criada.
- [ ] Roles criados.
- [ ] Nenhuma responsabilidade de EF/Http adicionada.

## PRÓXIMA TASK

TASK-007 — Criar entidade Client PF/PJ.

# TASK-007 — Criar entidade Client PF/PJ

STATUS: READY

EPIC: EPIC-001

FEATURE: FEAT-001.3

SIZE: S

DEPENDS ON: TASK-001, DEC-003, DEC-004, DEC-006

## CONTEXTO

A especificação define Client com `Id`, `Type`, `Document`, `Name`, `Email` e `Phone`, e confirma PF/PJ. Formato de documento, normalização, unicidade e campos condicionais não foram definidos.

## OBJETIVO

Criar a entidade Client e o tipo PF/PJ sem inventar regras de documento.

## O QUE VOCÊ DEVE FAZER

1. Criar entidade Client em Domain.
2. Criar tipo PF/PJ conforme nomenclatura aprovada.
3. Adicionar somente os campos confirmados.
4. Criar invariantes aprovadas em DEC-006.
5. Criar testes de tipo e construção.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Domain/`

## NÃO FAÇA

- Não validar CPF/CNPJ sem decisão.
- Não adicionar endereço, razão social ou score.
- Não mapear EF ou criar endpoint.

## REGRAS TÉCNICAS

- PF e PJ são confirmados.
- Document, Name, Email e Phone pertencem à entidade.
- Regras de required/unique são DEC-006.

## CRITÉRIOS DE ACEITE

- [ ] Entidade contém os seis campos da especificação.
- [ ] PF/PJ está represented.
- [ ] Nenhum campo ou constraint foi inventado.
- [ ] Testes unitários passam.

## TESTES OBRIGATÓRIOS

- Client PF válido.
- Client PJ válido.
- Tipo inválido.
- Campos conforme decisão.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Entidade e enum.
- Testes.
- DEC-006 referenciada.

## DEFINITION OF DONE

- [ ] Client criado.
- [ ] PF/PJ modelados.
- [ ] Regras não aprovadas ausentes.

## PRÓXIMA TASK

TASK-008 — Criar entidade Product sem estoque.

# TASK-008 — Criar entidade Product sem estoque

STATUS: READY

EPIC: EPIC-001

FEATURE: FEAT-001.3

SIZE: S

DEPENDS ON: TASK-001, DEC-003, DEC-004, DEC-007

## CONTEXTO

A especificação define Product com `Id`, `Name`, `Description` e `Price`, e confirma que produtos não têm estoque no MVP. Moeda, escala e unicidade de nome são pendentes.

## OBJETIVO

Criar a entidade Product sem campos ou comportamento de estoque.

## O QUE VOCÊ DEVE FAZER

1. Criar Product em Domain.
2. Mapear somente `Id`, `Name`, `Description` e `Price`.
3. Adicionar invariantes de preço aprovadas em DEC-007.
4. Criar testes de entidade.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Domain/`

## NÃO FAÇA

- Não criar Quantity, Stock, SKU ou variants.
- Não definir moeda/rounding sem DEC-007.
- Não mapear EF.

## REGRAS TÉCNICAS

- Estoque está fora do MVP.
- Price deve usar o tipo aprovado em DEC-007.

## CRITÉRIOS DE ACEITE

- [ ] Product contém os quatro campos confirmados.
- [ ] Nenhum campo de estoque existe.
- [ ] Price segue a decisão aprovada.
- [ ] Testes passam.

## TESTES OBRIGATÓRIOS

- Produto válido.
- Price inválido conforme decisão.
- Descrição/name conforme contrato.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Entidade.
- Testes.
- DEC-007 aplicada.

## DEFINITION OF DONE

- [ ] Product criado.
- [ ] Escopo sem estoque respeitado.
- [ ] Nenhuma decisão implícita.

## PRÓXIMA TASK

TASK-009 — Criar entidades Order e OrderItem.

# TASK-009 — Criar entidades Order e OrderItem

STATUS: READY

EPIC: EPIC-001

FEATURE: FEAT-001.3

SIZE: M

DEPENDS ON: TASK-001, DEC-003, DEC-004, DEC-008, DEC-009

## CONTEXTO

A especificação define Order (`Id`, `ClientId`, `Status`, `TotalAmount`) e OrderItem (`Id`, `OrderId`, `ProductId`, `Quantity`, `UnitPrice`, `TotalPrice`). Só os status Pendente, Concluído e Cancelado estão confirmados; transições, quantidade, snapshot e total são pendentes.

## OBJETIVO

Criar as entidades de pedido e seus relacionamentos de domínio sem implementar ainda a API ou cálculo final.

## O QUE VOCÊ DEVE FAZER

1. Criar Order e OrderItem em Domain.
2. Criar enum com os três status confirmados.
3. Adicionar navigações Client/Product conforme relacionamentos da especificação.
4. Implementar apenas invariantes aprovadas.
5. Criar testes de status e construção.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Domain/`

## NÃO FAÇA

- Não adicionar status adicional.
- Não implementar transições sem DEC-008.
- Não definir cálculo de total sem DEC-007/009.
- Não mapear EF.

## REGRAS TÉCNICAS

- Client possui N Orders.
- Order possui N OrderItems.
- OrderItem pertence a um Product.
- Estoque não participa do pedido.

## CRITÉRIOS DE ACEITE

- [ ] Order e OrderItem possuem os campos confirmados.
- [ ] Os três status estão presentes.
- [ ] Relacionamentos de navegação estão coerentes.
- [ ] Nenhuma regra não aprovada foi criada.

## TESTES OBRIGATÓRIOS

- Construção de Order com itens.
- Status válido.
- Quantidade/construtor conforme DEC-009.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Entidades e enum.
- Testes.
- Referência às decisões de status/total.

## DEFINITION OF DONE

- [ ] Entidades criadas.
- [ ] Relacionamentos modelados.
- [ ] Regras pendentes não foram inventadas.

## PRÓXIMA TASK

TASK-010 — Configurar DbContext e relacionamentos.

# TASK-010 — Configurar DbContext e relacionamentos

STATUS: BLOCKED

EPIC: EPIC-001

FEATURE: FEAT-001.3

SIZE: M

DEPENDS ON: TASK-003, TASK-006, TASK-007, TASK-008, TASK-009, DEC-004, DEC-005

## CONTEXTO

As entidades de domínio serão criadas, mas o modelo EF, as chaves estrangeiras, índices, query filters e delete behavior ainda não existem.

## OBJETIVO

Configurar o modelo relacional PostgreSQL de acordo com os relacionamentos confirmados e as decisões de integridade.

## O QUE VOCÊ DEVE FAZER

1. Adicionar DbSets no DbContext.
2. Configurar primary keys e nullability.
3. Configurar Client 1-N Order.
4. Configurar Order 1-N OrderItem.
5. Configurar OrderItem N-1 Product.
6. Configurar query filter de soft delete apenas nas entidades aprovadas.
7. Configurar delete behavior e índices somente após DEC-004/005.
8. Criar teste de criação do model.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Infrastructure/Persistence/`
- `backend/src/EnterpriseManagement.Infrastructure/`

## NÃO FAÇA

- Não criar migration ainda.
- Não escolher cascade/restrict sem decisão.
- Não adicionar timestamps a entidades sem decisão.
- Não criar seed.

## REGRAS TÉCNICAS

- Npgsql/EF Core são a persistência especificada.
- Query normal não deve retornar registros soft-deleted, se aprovado.
- FKs devem preservar integridade conforme DEC-005.

## CRITÉRIOS DE ACEITE

- [ ] Model creation não lança erro.
- [ ] Chaves e FKs correspondem à especificação.
- [ ] Relacionamentos são navegáveis e testáveis.
- [ ] Soft delete e delete behavior correspondem às decisões.

## TESTES OBRIGATÓRIOS

- Teste de model creation.
- Teste de relationships Client/Order/OrderItem/Product.
- Teste de query filter, quando aprovado.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Configurações EF.
- Teste de model.
- Decisões DEC-004/005.

## DEFINITION OF DONE

- [ ] DbContext completo.
- [ ] Relationships testados.
- [ ] Delete behavior aprovado e documentado.

## PRÓXIMA TASK

TASK-011 — Criar migration e seed aprovados.

# TASK-011 — Criar migration e seed aprovados

STATUS: BLOCKED

EPIC: EPIC-001

FEATURE: FEAT-001.3

SIZE: M

DEPENDS ON: TASK-010, DEC-010, DEC-020, DEC-023

## CONTEXTO

O banco PostgreSQL é requisito, mas ainda não há migration, banco de teste ou seed. O modelo de User inclui refresh token; o bootstrap de ADMIN não foi definido.

## OBJETIVO

Gerar a migration inicial e o seed somente após a aprovação do schema e da estratégia de ambiente.

## O QUE VOCÊ DEVE FAZER

1. Configurar conexão de design-time conforme decisão.
2. Gerar migration inicial.
3. Verificar tabelas, FKs, constraints e índices gerados.
4. Criar seed apenas se DEC-010 e DEC-023 forem aprovados.
5. Testar aplicação em banco vazio de teste.
6. Documentar apply, rollback e reset seguro.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Infrastructure/Migrations/`
- `backend/src/EnterpriseManagement.Api/appsettings*.json`
- Scripts de setup
- `README.md` ou documentação de ambiente

## NÃO FAÇA

- Não executar migration destrutiva em banco existente.
- Não apagar banco de desenvolvimento.
- Não criar senha ADMIN padrão.
- Não usar seed com segredo em source.

## REGRAS TÉCNICAS

- PostgreSQL é o provider especificado.
- Refresh token deve ser persistido.
- Banco de teste deve ser isolado.
- Seed segue DEC-010 e DEC-023 aprovadas.

## CRITÉRIOS DE ACEITE

- [ ] Migration aplica em banco vazio.
- [ ] Schema corresponde ao model aprovado.
- [ ] Seed contém somente dados aprovados.
- [ ] Procedimento de reset exige autorização.

## TESTES OBRIGATÓRIOS

- Teste de migration em banco vazio.
- Teste de relationships e constraints.
- Teste de seed, se existir.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

O comando de migration deve ser definido na TASK-014/023 e não é inventado aqui.

## EVIDÊNCIAS

- Migration ID.
- Log de aplicação.
- Schema ou testes de integridade.
- Decisões de seed e ambiente.

## DEFINITION OF DONE

- [ ] Migration criada e testada.
- [ ] Seed seguro ou explicitamente omitido.
- [ ] Setup documentado.

## PRÓXIMA TASK

TASK-012 — Completar shell e providers da SPA.

# TASK-012 — Completar shell e providers da SPA

STATUS: READY

EPIC: EPIC-001

FEATURE: FEAT-001.5

SIZE: M

DEPENDS ON: TASK-001, DEC-018, DEC-022

## CONTEXTO

O projeto Vite já inicializa `App.tsx`, mas `AppRoutes` não é funcional e os providers de router, query, tema e error boundary não estão conectados. A tarefa deve completar o shell existente, não recriar o projeto.

## OBJETIVO

Fazer a SPA iniciar com roteamento, layout, tema, providers e estados globais prontos para as páginas do MVP.

## O QUE VOCÊ DEVE FAZER

1. Configurar o router conforme DEC-018.
2. Conectar TanStack Query.
3. Configurar tema Material UI conforme padrão existente.
4. Criar layout público e autenticado.
5. Criar error boundary e fallback de rota.
6. Criar pastas conforme a especificação.
7. Manter lint e build passando.

## ONDE TRABALHAR

- `frontend/src/main.tsx`
- `frontend/src/App.tsx`
- `frontend/src/routes/`
- `frontend/src/layouts/`
- `frontend/src/theme/`
- `frontend/src/app/`

## NÃO FAÇA

- Não implementar páginas de Login, Users, Clients, Products, Orders ou Dashboard.
- Não implementar API client.
- Não escolher paths sem DEC-018.
- Não introduzir biblioteca nova sem necessidade.

## REGRAS TÉCNICAS

- React Router, Axios, TanStack Query, React Hook Form, Zod e Material UI já estão declarados.
- Estrutura de pastas deve seguir a especificação.
- A UI não é a fronteira de autorização.

## CRITÉRIOS DE ACEITE

- [ ] App inicia sem erro.
- [ ] Router e QueryClient estão registrados uma vez.
- [ ] Layout público e autenticado são distinguíveis.
- [ ] Fallback e error boundary existem.
- [ ] `npm run lint` e `npm run build` passam.

## TESTES OBRIGATÓRIOS

- Smoke de renderização do App.
- Teste de rota fallback.
- Teste de provider, quando infraestrutura de testes existir.

## COMO VALIDAR

```text
npm run lint
npm run build
```

## EVIDÊNCIAS

- Rotas renderizadas.
- Saída de lint/build.
- Estrutura de pastas.

## DEFINITION OF DONE

- [ ] Shell navegável.
- [ ] Providers conectados.
- [ ] Nenhuma feature implementada fora do escopo.

## PRÓXIMA TASK

TASK-013 — Criar API client e tratamento de erros.

# TASK-013 — Criar API client e tratamento de erros

STATUS: BLOCKED

EPIC: EPIC-001

FEATURE: FEAT-001.5

SIZE: S

DEPENDS ON: TASK-004, DEC-001, DEC-013

## CONTEXTO

`frontend/src/services/http.ts` está vazio. A API client precisa centralizar URL, erros e estados de loading, mas o armazenamento de token depende de DEC-013.

## OBJETIVO

Criar uma camada HTTP reutilizável sem incluir armazenamento de token não aprovado.

## O QUE VOCÊ DEVE FAZER

1. Criar instância Axios ou client equivalente já declarado.
2. Configurar base URL por ambiente.
3. Normalizar erros conforme TASK-004.
4. Diferenciar 401, 403, 404, 409 e 5xx.
5. Implementar cancelamento/loading conforme padrão aprovado.
6. Criar testes com respostas simuladas.

## ONDE TRABALHAR

- `frontend/src/services/http.ts`
- `frontend/src/types/`
- `frontend/src/utils/`
- Testes em `frontend/src/`

## NÃO FAÇA

- Não guardar refresh token em localStorage sem DEC-013.
- Não implementar login/logout.
- Não inventar error format.
- Não fazer chamadas de negócio nesta task.

## REGRAS TÉCNICAS

- Nenhum token ou secret deve aparecer no bundle sem decisão.
- Erros devem seguir DEC-001.
- A URL da API vem de ambiente.

## CRITÉRIOS DE ACEITE

- [ ] Base URL configurável.
- [ ] Erros normalizados.
- [ ] Status HTTP não são tratados como sucesso indevidamente.
- [ ] Testes de sucesso/erro existem.

## TESTES OBRIGATÓRIOS

- Request com sucesso.
- 400/401/403/404/409/500.
- Cancelamento ou loading, se implementado.

## COMO VALIDAR

```text
npm run lint
npm run build
```

O script `npm test` estará disponível somente após TASK-015.

## EVIDÊNCIAS

- Testes do client.
- Configuração de URL.
- Exemplo sanitizado de erro.

## DEFINITION OF DONE

- [ ] Client HTTP criado.
- [ ] Erros centralizados.
- [ ] Token não armazenado indevidamente.

## PRÓXIMA TASK

TASK-014 — Criar infraestrutura de testes backend.

# TASK-014 — Criar infraestrutura de testes backend

STATUS: READY

EPIC: EPIC-001

FEATURE: FEAT-001.6

SIZE: M

DEPENDS ON: TASK-001, DEC-020, DEC-021, DEC-022

## CONTEXTO

A especificação exige xUnit, testes unitários e integração. Não há projeto de teste, banco de teste ou comando dedicado.

## OBJETIVO

Criar a infraestrutura mínima para que future tasks de backend tenham testes executáveis e isolados.

## O QUE VOCÊ DEVE FAZER

1. Criar projeto xUnit de unit tests.
2. Criar projeto de integração conforme DEC-020.
3. Adicionar referência aos projetos necessários.
4. Criar fixtures e factory de banco sem segredo.
5. Configurar limpeza/cleanup seguro do banco de teste.
6. Integrar os projetos à solução.
7. Documentar o comando de teste.

## ONDE TRABALHAR

- `backend/tests/`
- `backend/EnterpriseManagement.slnx`
- Scripts de teste, se necessário

## NÃO FAÇA

- Não usar banco de produção.
- Não apagar banco de desenvolvimento.
- Não escolher Docker/Postgres container sem DEC-020.
- Não escrever todos os testes de negócio nesta task.

## REGRAS TÉCNICAS

- xUnit é a ferramenta especificada.
- Banco de teste deve ser isolado.
- Testes devem ser reproduzíveis e não destrutivos.

## CRITÉRIOS DE ACEITE

- [ ] `dotnet test` encontra os projetos criados.
- [ ] Banco de teste é isolado.
- [ ] Fixtures podem ser reutilizadas.
- [ ] Comando e pré-requisitos documentados.

## TESTES OBRIGATÓRIOS

- Teste smoke de teste unitário.
- Teste smoke de fixture de integração.
- Teste de isolamento do banco.

## COMO VALIDAR

```text
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- `.csproj` de teste.
- Saída do comando.
- Configuração do banco de teste.

## DEFINITION OF DONE

- [ ] Infraestrutura de testes criada.
- [ ] `dotnet test` executável.
- [ ] Nenhum banco real destruído.

## PRÓXIMA TASK

TASK-015 — Criar infraestrutura Vitest/RTL e Playwright.

# TASK-015 — Criar infraestrutura Vitest/RTL e Playwright

STATUS: READY

EPIC: EPIC-001

FEATURE: FEAT-001.6

SIZE: M

DEPENDS ON: TASK-001, DEC-020, DEC-021, DEC-022

## CONTEXTO

A especificação exige Vitest, React Testing Library e Playwright. O `package.json` possui apenas `dev`, `build`, `lint` e `preview`.

## OBJETIVO

Adicionar configuração e scripts de teste frontend/E2E sem escrever os fluxos de negócio.

## O QUE VOCÊ DEVE FAZER

1. Adicionar Vitest e React Testing Library.
2. Criar setup de DOM, mocks e cleanup.
3. Adicionar script de teste frontend.
4. Adicionar Playwright e configuração base.
5. Definir webServer e banco de teste conforme DEC-020.
6. Criar smoke tests de configuração.
7. Documentar `npm test` e `npx playwright test`.

## ONDE TRABALHAR

- `frontend/package.json`
- `frontend/package-lock.json`
- `frontend/vitest.config.*`
- `frontend/playwright.config.*`
- `frontend/src/` setup de testes

## NÃO FAÇA

- Não implementar todos os fluxos E2E.
- Não inventar comandos antes de adicionar os scripts.
- Não usar dados de produção.
- Não escolher plataforma de CI.

## REGRAS TÉCNICAS

- Vitest e React Testing Library são tecnologias especificadas.
- Playwright deve cobrir os seis fluxos quando os testes forem escritos.
- Testes devem ser isolados e determinísticos.

## CRITÉRIOS DE ACEITE

- [ ] `npm test` executa a suíte configurada.
- [ ] `npx playwright test` está configurado.
- [ ] Setup de DOM e mocks funciona.
- [ ] Ambiente de teste é reproduzível.
- [ ] Lint e build continuam passando.

## TESTES OBRIGATÓRIOS

- Smoke de renderização.
- Smoke de Playwright sem cenário de negócio.
- Teste de cleanup de mocks.

## COMO VALIDAR

```text
npm test
npm run lint
npm run build
npx playwright test
```

## EVIDÊNCIAS

- Configuração de Vitest/RTL/Playwright.
- Saídas dos comandos.
- Configuração do ambiente.

## DEFINITION OF DONE

- [ ] Infraestrutura frontend/E2E pronta.
- [ ] Scripts documentados.
- [ ] Nenhum fluxo de negócio implementado.

## PRÓXIMA TASK

TASK-016 — Implementar BCrypt password hasher.

# TASK-016 — Implementar BCrypt password hasher

STATUS: BLOCKED

EPIC: EPIC-002

FEATURE: FEAT-002.1

SIZE: S

DEPENDS ON: TASK-002, DEC-010

## CONTEXTO

A especificação exige BCrypt para senhas. User possui `PasswordHash`, mas a política de senha, custo do hash e seed não estão definidos.

## OBJETIVO

Criar uma abstração de password hasher e uma implementação BCrypt integrada aos casos de uso, sem plaintext.

## O QUE VOCÊ DEVE FAZER

1. Definir interface de hash e verify na camada apropriada.
2. Implementar BCrypt em Infrastructure.
3. Integrar hash no create/update de User.
4. Aplicar custo aprovado em DEC-010.
5. Criar testes de hash, verify e senha incorreta.
6. Garantir que hash não seja logado ou retornado.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Application/`
- `backend/src/EnterpriseManagement.Infrastructure/`
- `backend/tests/`

## NÃO FAÇA

- Não criar endpoint de login nesta task.
- Não definir política de senha sem DEC-010.
- Não usar hash customizado sem decisão.
- Não registrar senha em logs.

## REGRAS TÉCNICAS

- BCrypt é requisito da especificação.
- PasswordHash é o único valor de senha persistido.
- O hasher deve ser testável sem banco.

## CRITÉRIOS DE ACEITE

- [ ] Hash gerado não é igual à senha plaintext.
- [ ] Verify aceita somente o hash correspondente.
- [ ] Parâmetros de custo seguem DEC-010.
- [ ] Senha e hash não aparecem em logs ou responses.

## TESTES OBRIGATÓRIOS

- Hash e verify positivo.
- Verify negativo.
- Hash não reversível para plaintext.
- Entrada vazia/inválida conforme validação.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Interface e implementação.
- Testes unitários.
- Configuração de custo.

## DEFINITION OF DONE

- [ ] BCrypt integrado.
- [ ] Testes passam.
- [ ] Nenhum secret exposto.

## PRÓXIMA TASK

TASK-017 — Configurar serviço JWT e claims.

# TASK-017 — Configurar serviço JWT e claims

STATUS: BLOCKED

EPIC: EPIC-002

FEATURE: FEAT-002.2

SIZE: M

DEPENDS ON: TASK-002, DEC-011

## CONTEXTO

A especificação exige JWT assinado, expiração de 15 minutos e claims `sub`, `email` e `role`. Algoritmo, issuer, audience e management da signing key são pendentes.

## OBJETIVO

Criar configuração e serviço de emissão/validação de JWT sem expor a chave.

## O QUE VOCÊ DEVE FAZER

1. Criar options de JWT conforme DEC-011.
2. Implementar emissão com os três claims confirmados.
3. Implementar validação de assinatura, claims e expiração.
4. Configurar clock/expiration para 15 minutos.
5. Ler signing key de configuração externa.
6. Criar testes de token válido, expirado e inválido.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Infrastructure/Security/`
- `backend/src/EnterpriseManagement.Application/`
- `backend/src/EnterpriseManagement.Api/`
- `backend/tests/`

## NÃO FAÇA

- Não usar chave fixa em source.
- Não definir algoritmo/issuer/audience sem decisão.
- Não implementar refresh token nesta task.
- Não adicionar claims além dos confirmados sem decisão.

## REGRAS TÉCNICAS

- Access token expira em 15 minutos.
- Claims obrigatórios: `sub`, `email`, `role`.
- JWT deve ser assinado.

## CRITÉRIOS DE ACEITE

- [ ] Token válido é aceito.
- [ ] Token expirado é rejeitado.
- [ ] Token com assinatura incorreta é rejeitado.
- [ ] Claims aparecem no payload.
- [ ] Chave não está no repositório.

## TESTES OBRIGATÓRIOS

- Emissão com claims corretos.
- Validação de expiração.
- Validação de assinatura.
- Claims ausentes ou incorretos.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Configuração.
- Testes de token.
- Exemplo de payload sanitizado.

## DEFINITION OF DONE

- [ ] Serviço JWT testado.
- [ ] Configuração segura.
- [ ] Requisitos de claims/expiração atendidos.

## PRÓXIMA TASK

TASK-018 — Implementar login no backend.

# TASK-018 — Implementar login no backend

STATUS: BLOCKED

EPIC: EPIC-002

FEATURE: FEAT-002.1

SIZE: M

DEPENDS ON: TASK-004, TASK-010, TASK-016, TASK-017, DEC-003, DEC-010, DEC-011

## CONTEXTO

A rota `POST /api/auth/login` está definida, mas request, response e status de erro não. O login precisa consultar User, verificar BCrypt e emitir os tokens segundo o contrato aprovado.

## OBJETIVO

Entregar o caso de uso e endpoint de login válidos e proteger contra usuário inexistente, senha inválida e usuário deletado.

## O QUE VOCÊ DEVE FAZER

1. Criar request/response conforme DEC-003.
2. Validar credenciais.
3. Consultar User pelo identificador de login aprovado.
4. Excluir usuário soft-deleted da autenticação.
5. Verificar senha com BCrypt.
6. Emitir access e refresh tokens conforme contratos.
7. Retornar somente dados seguros.
8. Criar testes de integração.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Application/Auth/`
- `backend/src/EnterpriseManagement.Api/Auth/`
- DTOs, validators e testes de integração

## NÃO FAÇA

- Não implementar refresh, logout ou RBAC nesta task.
- Não retornar PasswordHash.
- Não criar resposta nova sem DEC-001/003.
- Não registrar senha/token em log.

## REGRAS TÉCNICAS

- Login é público.
- BCrypt é obrigatório.
- Access token expira em 15 minutos.
- Refresh token tem sete dias.

## CRITÉRIOS DE ACEITE

- [ ] Credenciais válidas emitem sessão.
- [ ] Credenciais inválidas não emitem tokens.
- [ ] Usuário deletado não autentica.
- [ ] Resposta segue o contrato aprovado.
- [ ] Erros não revelam dados sensíveis.

## TESTES OBRIGATÓRIOS

- Happy path.
- Email inexistente.
- Senha incorreta.
- Usuário soft-deleted.
- Payload inválido.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Testes de integração.
- Response sanitizada.
- Log de auditoria sem credenciais.

## DEFINITION OF DONE

- [ ] Login funciona.
- [ ] Erros documentados.
- [ ] Nenhum segredo exposto.

## PRÓXIMA TASK

TASK-019 — Implementar persistência e ciclo do refresh token.

# TASK-019 — Implementar persistência e ciclo do refresh token

STATUS: BLOCKED

EPIC: EPIC-002

FEATURE: FEAT-002.3

SIZE: M

DEPENDS ON: TASK-006, TASK-010, DEC-012

## CONTEXTO

User possui `RefreshToken` e `RefreshTokenExpiresAt`; a especificação exige sete dias, persistência e revogação. Rotação, reuso e efeito de logout não estão definidos.

## OBJETIVO

Implementar o ciclo de vida do refresh token no banco, com estratégia aprovada e sem permitir renovação de token revogado.

## O QUE VOCÊ DEVE FAZER

1. Criar service de emissão e persistência do refresh token.
2. Aplicar expiração de sete dias.
3. Implementar rotação/reuso conforme DEC-012.
4. Implementar revogação.
5. Definir concorrência e repeatable behavior.
6. Criar testes de persistência, expiração, revogação e reuso.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Domain/`
- `backend/src/EnterpriseManagement.Application/Auth/`
- `backend/src/EnterpriseManagement.Infrastructure/Persistence/`
- Testes de integração

## NÃO FAÇA

- Não criar endpoint nesta task.
- Não adicionar tabela de refresh token sem decisão.
- Não aceitar token expirado ou revogado.
- Não implementar storage frontend.

## REGRAS TÉCNICAS

- Persistência no PostgreSQL é obrigatória.
- Validade é sete dias.
- Revogabilidade é obrigatória.
- Estratégia de rotação/reuso é DEC-012.

## CRITÉRIOS DE ACEITE

- [ ] Token válido é persistido.
- [ ] Token expirado é rejeitado.
- [ ] Token revogado é rejeitado.
- [ ] Reuso/rotação segue DEC-012.
- [ ] Alterações são transacionais quando aprovadas.

## TESTES OBRIGATÓRIOS

- Emitir e persistir.
- Expirar.
- Revogar.
- Reutilizar token.
- Executar duas renovações concorrentes.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Testes de persistência.
- Query do token sem logs de valor completo.
- ADR DEC-012 aplicada.

## DEFINITION OF DONE

- [ ] Refresh token persistido.
- [ ] Revogação testada.
- [ ] Expiração de sete dias atendida.

## PRÓXIMA TASK

TASK-020 — Implementar endpoint de refresh.

# TASK-020 — Implementar endpoint de refresh

STATUS: BLOCKED

EPIC: EPIC-002

FEATURE: FEAT-002.3

SIZE: S

DEPENDS ON: TASK-004, TASK-019

## CONTEXTO

A rota `POST /api/auth/refresh` está definida, mas o formato do request e response não. O endpoint deve delegar o lifecycle ao serviço de TASK-019.

## OBJETIVO

Expor a renovação de access token com contrato documentado e sem aceitar token inválido.

## O QUE VOCÊ DEVE FAZER

1. Criar DTO de request e response.
2. Validar o refresh token.
3. Chamar o service de lifecycle.
4. Retornar tokens conforme DEC-012.
5. Mapear token inválido/expirado/revogado para status aprovado.
6. Adicionar endpoint ao OpenAPI.
7. Criar testes de integração.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Api/Auth/`
- DTOs, OpenAPI e testes

## NÃO FAÇA

- Não implementar a lógica de rotação no controller/endpoint.
- Não aceitar token revogado.
- Não criar logout.
- Não retornar dados de User além do contrato.

## REGRAS TÉCNICAS

- Refresh token é persistido e revogável.
- Token inválido deve falhar.
- Erro segue TASK-004.

## CRITÉRIOS DE ACEITE

- [ ] Token válido renova a sessão.
- [ ] Token expirado/revogado é rejeitado.
- [ ] Resposta segue contrato.
- [ ] Endpoint está documentado.

## TESTES OBRIGATÓRIOS

- Happy path.
- Token expirado.
- Token revogado.
- Payload ausente/malformado.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Testes de endpoint.
- Response sanitizada.
- OpenAPI atualizado.

## DEFINITION OF DONE

- [ ] Endpoint implementado.
- [ ] Erros testados.
- [ ] Contrato publicado.

## PRÓXIMA TASK

TASK-021 — Implementar logout e revogação.

# TASK-021 — Implementar logout e revogação

STATUS: BLOCKED

EPIC: EPIC-002

FEATURE: FEAT-002.3

SIZE: S

DEPENDS ON: TASK-019, TASK-022, DEC-012

## CONTEXTO

A rota `POST /api/auth/logout` está definida. Autenticação do logout, idempotência e escopo da revogação são decisões pendentes.

## OBJETIVO

Encerrar a sessão e impedir que o refresh token utilize possa renovar a sessão.

## O QUE VOCÊ DEVE FAZER

1. Definir request/authentication conforme DEC-012.
2. Chamar o service de revogação.
3. Definir resposta de sucesso e comportamento idempotente.
4. Adicionar OpenAPI.
5. Criar teste de logout seguido de refresh.
6. Garantir que o token não seja registrado em log.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Api/Auth/`
- `Application/Auth/`
- Testes de integração

## NÃO FAÇA

- Não revogar sessões não aprovadas.
- Não criar endpoint de login.
- Não alterar o lifecycle de refresh sem decisão.

## REGRAS TÉCNICAS

- Logout deve revogar o token da sessão.
- Revogação é requisito da especificação.
- Respostas não expõem tokens.

## CRITÉRIOS DE ACEITE

- [ ] Logout válido revoga a sessão.
- [ ] Refresh posterior é rejeitado.
- [ ] Repetição tem comportamento documentado.
- [ ] Token não aparece em logs.

## TESTES OBRIGATÓRIOS

- Logout e refresh posterior.
- Token inválido.
- Logout repetido.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Testes de logout.
- Response sanitizada.
- Contrato de API.

## DEFINITION OF DONE

- [ ] Logout implementado.
- [ ] Revogação comprovada.
- [ ] Erros documentados.

## PRÓXIMA TASK

TASK-022 — Configurar middleware JWT e policies RBAC.

# TASK-022 — Configurar middleware JWT e policies RBAC

STATUS: BLOCKED

EPIC: EPIC-002

FEATURE: FEAT-002.4

SIZE: M

DEPENDS ON: TASK-017, DEC-010, DEC-011

## CONTEXTO

A matriz da especificação permite todas as áreas a ADMIN e somente Clients, Products, Orders e Dashboard a OPERATOR. Não há middleware, policies ou claims processing.

## OBJETIVO

Proteger a API no backend com autenticação e autorização baseadas na matriz de roles.

## O QUE VOCÊ DEVE FAZER

1. Registrar autenticação JWT no host.
2. Registrar authorization e policies ADMIN/OPERATOR.
3. Ler `role` do claim aprovado.
4. Aplicar policies aos endpoints dos módulos.
5. Definir 401 para token ausente/inválido e 403 para role insuficiente.
6. Criar matriz de testes por endpoint.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Api/Program.cs`
- `backend/src/EnterpriseManagement.Api/Authorization/`
- Endpoints futuros
- Testes de integração

## NÃO FAÇA

- Não usar botão do frontend como autorização.
- Não criar roles além de ADMIN e OPERATOR.
- Não remover policy para simplificar a UI.
- Não tratar token expirado como autenticado.

## REGRAS TÉCNICAS

- ADMIN: Users, Clients, Products, Orders, Dashboard.
- OPERATOR: Clients, Products, Orders, Dashboard.
- Autorização deve ocorrer no backend.
- 401/403 devem ser testados.

## CRITÉRIOS DE ACEITE

- [ ] Token válido identifica role.
- [ ] ADMIN acessa todas as áreas.
- [ ] OPERATOR recebe 403 em Users.
- [ ] Token ausente/inválido recebe 401.
- [ ] Handler não executa antes da autorização.

## TESTES OBRIGATÓRIOS

- Matriz completa role × endpoint.
- Token expirado.
- Token com role inválida.
- Usuário sem token.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Matriz de policies.
- Testes 401/403.
- Logs sem token completo.

## DEFINITION OF DONE

- [ ] RBAC aplicado no backend.
- [ ] Matriz testada.
- [ ] UI não é security boundary.

## PRÓXIMA TASK

TASK-023 — Criar contexto de autenticação frontend.

# TASK-023 — Criar contexto de autenticação frontend

STATUS: BLOCKED

EPIC: EPIC-002

FEATURE: FEAT-002.5

SIZE: M

DEPENDS ON: TASK-013, TASK-018, TASK-020, TASK-021, DEC-013

## CONTEXTO

A SPA não possui contexto, hook ou estado de sessão. O armazenamento de access/refresh token é DEC-013.

## OBJETIVO

Centralizar estado de autenticação, login, refresh, logout e tratamento de expiração no frontend.

## O QUE VOCÊ DEVE FAZER

1. Criar AuthContext e hook de acesso.
2. Integrar services de login, refresh e logout.
3. Armazenar tokens conforme DEC-013.
4. Adicionar interceptor de 401.
5. Evitar refresh loop e requests duplicados.
6. Limpar sessão em falha de refresh.
7. Criar testes de context e service.

## ONDE TRABALHAR

- `frontend/src/contexts/`
- `frontend/src/hooks/`
- `frontend/src/services/`
- `frontend/src/types/`

## NÃO FAÇA

- Não escolher localStorage/cookie sem DEC-013.
- Não implementar páginas de login nesta task.
- Não duplicar lógica de autorização do backend.
- Não registrar tokens no console.

## REGRAS TÉCNICAS

- Access token expira em 15 minutos.
- Refresh dura sete dias.
- Storage e rotação são decisões explícitas.

## CRITÉRIOS DE ACEITE

- [ ] Estado authenticated/unauthenticated é observável.
- [ ] Login e logout alteram o estado.
- [ ] 401 tenta refresh apenas uma vez conforme decisão.
- [ ] Falha de refresh limpa a sessão.
- [ ] Tokens não aparecem em logs.

## TESTES OBRIGATÓRIOS

- Login com sucesso.
- Login com erro.
- Refresh bem-sucedido.
- Refresh falho.
- Logout e redirect.

## COMO VALIDAR

```text
npm run lint
npm run build
```

O comando `npm test` estará disponível após TASK-015.

## EVIDÊNCIAS

- Testes do contexto.
- Configuração de storage.
- Evidência de logout/refresh.

## DEFINITION OF DONE

- [ ] Auth state testado.
- [ ] Storage documentado.
- [ ] Redirects implementados.

## PRÓXIMA TASK

TASK-024 — Criar login e protected routes.

# TASK-024 — Criar login e protected routes

STATUS: BLOCKED

EPIC: EPIC-002

FEATURE: FEAT-002.5

SIZE: M

DEPENDS ON: TASK-012, TASK-022, TASK-023, DEC-018

## CONTEXTO

Não existe página de Login nem protected route. A API deve continuar sendo a fonte de autorização, mesmo que a SPA oculte controles.

## OBJETIVO

Permitir login e bloquear navegação não autorizada conforme sessão e role.

## O QUE VOCÊ DEVE FAZER

1. Criar página/formulário de Login.
2. Validar request conforme DEC-003.
3. Exibir loading, erro e sucesso.
4. Redirecionar após login.
5. Criar ProtectedRoute.
6. Bloquear `/users` para OPERATOR conforme rota aprovada.
7. Redirecionar sessão expirada para Login.
8. Criar testes de componente.

## ONDE TRABALHAR

- `frontend/src/pages/`
- `frontend/src/routes/`
- `frontend/src/layouts/`
- `frontend/src/validations/`

## NÃO FAÇA

- Não implementar recuperação de senha.
- Não criar tela de perfil.
- Não substituir a policy backend.
- Não escolher paths sem DEC-018.

## REGRAS TÉCNICAS

- Login é público.
- Área autenticada exige sessão.
- Users exige ADMIN.
- OPERATOR não deve acessar Users.

## CRITÉRIOS DE ACEITE

- [ ] Login válido redireciona.
- [ ] Login inválido exibe erro e não cria sessão.
- [ ] Rota sem sessão redireciona.
- [ ] OPERATOR não navega para Users.
- [ ] Backend continua rejeitando acesso direto.

## TESTES OBRIGATÓRIOS

- Formulário válido/inválido.
- Loading e erro.
- ProtectedRoute autenticada.
- ProtectedRoute sem sessão.
- Role insuficiente.

## COMO VALIDAR

```text
npm run lint
npm run build
```

## EVIDÊNCIAS

- Testes RTL.
- URL das rotas.
- Captura dos estados, se aplicável.

## DEFINITION OF DONE

- [ ] Login e rotas testados.
- [ ] Estados de UI cobertos.
- [ ] Autorização backend preservada.

## PRÓXIMA TASK

TASK-025 — Criar testes de autenticação e autorização.

# TASK-025 — Criar testes de autenticação e autorização

STATUS: BLOCKED

EPIC: EPIC-002

FEATURE: FEAT-002.6

SIZE: M

DEPENDS ON: TASK-014, TASK-015, TASK-018, TASK-019, TASK-020, TASK-021, TASK-022

## CONTEXTO

Login, refresh, logout, JWT e RBAC são os controles de maior risco. Nenhum teste automatizado existe.

## OBJETIVO

Criar uma suíte de autenticação que detecte regressões de segurança e comportamento.

## O QUE VOCÊ DEVE FAZER

1. Criar testes unitários de BCrypt e JWT.
2. Criar testes de integração de login/refresh/logout.
3. Criar matriz 401/403 por role.
4. Testar token expirado/revogado.
5. Testar CORS, headers e rate limit quando TASK-005 estiver concluída.
6. Criar testes de auth state no frontend.
7. Registrar evidências.

## ONDE TRABALHAR

- `backend/tests/Auth/`
- `backend/tests/Integration/`
- `frontend/src/**/*.test.*`

## NÃO FAÇA

- Não usar teste que passe sem verificar status/persistência.
- Não marcar PASS por build.
- Não testar banco de produção.
- Não esconder findings de segurança.

## REGRAS TÉCNICAS

- BCrypt, claims e expiração são requisitos.
- 401 e 403 têm semântica definida.
- Ausência de evidência é NOT_VERIFIED.

## CRITÉRIOS DE ACEITE

- [ ] Happy path de cada auth endpoint é coberto.
- [ ] Credenciais inválidas são cobertas.
- [ ] Refresh revogado/expirado é coberto.
- [ ] Matriz RBAC é coberta.
- [ ] Testes de segurança transversal existem ou estão explicitamente bloqueados.

## TESTES OBRIGATÓRIOS

- Unitários de token e hash.
- Integração de endpoints.
- RTL de AuthContext e Login.
- E2E de sessão quando TASK-015 estiver pronta.

## COMO VALIDAR

```text
dotnet test EnterpriseManagement.slnx
npm test
```

## EVIDÊNCIAS

- Relatório de testes.
- Nomes dos cenários.
- Requests/respostas sanitizadas.

## DEFINITION OF DONE

- [ ] Auth e RBAC testados.
- [ ] Findings registrados.
- [ ] Comandos executáveis.

## PRÓXIMA TASK

TASK-026 — Criar DTOs e validação de User.

# TASK-026 — Criar DTOs e validação de User

STATUS: BLOCKED

EPIC: EPIC-003

FEATURE: FEAT-003.1

SIZE: S

DEPENDS ON: TASK-004, TASK-006, DEC-003, DEC-010

## CONTEXTO

User possui campos de domínio, mas a API não possui DTOs nem regras de required, email unique ou password.

## OBJETIVO

Criar contratos de entrada/saída e validação de User sem retornar PasswordHash.

## O QUE VOCÊ DEVE FAZER

1. Criar DTOs de create, update, detail e list.
2. Definir required e lengths conforme DEC-003.
3. Validar role ADMIN/OPERATOR.
4. Definir email conforme DEC-010.
5. Remover PasswordHash de todos os responses.
6. Criar testes de validator e mapping.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Application/Users/`
- DTOs, validators e testes

## NÃO FAÇA

- Não implementar endpoint.
- Não inventar regex, tamanho ou unicidade.
- Não permitir alteração de IsDeleted por request comum.

## REGRAS TÉCNICAS

- Roles são ADMIN e OPERATOR.
- PasswordHash nunca é response.
- Email é decidido em DEC-010.

## CRITÉRIOS DE ACEITE

- [ ] DTOs cobrem operações necessárias.
- [ ] Campos inválidos são rejeitados.
- [ ] Role inválida é rejeitada.
- [ ] Hash não aparece no response.

## TESTES OBRIGATÓRIOS

- DTO válido.
- Name/email/role/password inválidos.
- Serialização sem PasswordHash.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- DTOs.
- Testes de validação.
- Contrato OpenAPI.

## DEFINITION OF DONE

- [ ] Contrato de User aprovado.
- [ ] Validação testada.
- [ ] Segredos não expostos.

## PRÓXIMA TASK

TASK-027 — Implementar consultas de User.

# TASK-027 — Implementar consultas de User

STATUS: BLOCKED

EPIC: EPIC-003

FEATURE: FEAT-003.1

SIZE: M

DEPENDS ON: TASK-010, TASK-026, DEC-002, DEC-004

## CONTEXTO

As APIs de listagem e detalhe de Users precisam de paginação, filtros e query de soft delete. O formato de paginação e o escopo do soft delete são decisões pendentes.

## OBJETIVO

Implementar services de listagem e consulta de User, sem endpoint nesta task.

## O QUE VOCÊ DEVE FAZER

1. Implementar list com paginação aprovada.
2. Implementar filtros aprovados.
3. Implementar detail por ID.
4. Aplicar query filter de IsDeleted.
5. Mapear para DTOs seguros.
6. Criar testes de lista vazia, página, filtro e not found.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Application/Users/`
- `backend/src/EnterpriseManagement.Infrastructure/Persistence/Users/`
- Testes

## NÃO FAÇA

- Não criar endpoints.
- Não retornar hash.
- Não inventar filtros ou defaults.

## REGRAS TÉCNICAS

- Paginação/filtros são diferenciais, mas formato é DEC-002.
- User deletado não deve aparecer em consulta normal, se aprovado.

## CRITÉRIOS DE ACEITE

- [ ] Lista respeita contrato de paginação.
- [ ] Filtros aprovados funcionam.
- [ ] ID inexistente retorna not found no serviço/API.
- [ ] Registros deletados ficam ocultos conforme decisão.

## TESTES OBRIGATÓRIOS

- Lista vazia.
- Página inicial e seguinte.
- Filtro com e sem resultado.
- Detalhe existente/inexistente.
- Usuário deletado.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Testes de integração.
- Query logs sem PII indevida.
- Contrato de paginação.

## DEFINITION OF DONE

- [ ] Consultas implementadas.
- [ ] Soft delete testado.
- [ ] Sem hash exposto.

## PRÓXIMA TASK

TASK-028 — Expor CRUD de Users para ADMIN.

# TASK-028 — Expor CRUD de Users para ADMIN

STATUS: BLOCKED

EPIC: EPIC-003

FEATURE: FEAT-003.1

SIZE: M

DEPENDS ON: TASK-022, TASK-026, TASK-027, DEC-001, DEC-004

## CONTEXTO

As rotas de Users são cinco e a matriz da especificação restringe Users a ADMIN. O backend ainda não possui endpoints de negócio.

## OBJETIVO

Implementar os endpoints de User com validação, autorização ADMIN, soft delete e OpenAPI.

## O QUE VOCÊ DEVE FAZER

1. Implementar `GET /api/users`.
2. Implementar `GET /api/users/{id}`.
3. Implementar `POST /api/users` com BCrypt.
4. Implementar `PUT /api/users/{id}`.
5. Implementar `DELETE /api/users/{id}` como soft delete aprovado.
6. Aplicar policy ADMIN em todos os endpoints.
7. Adicionar testes por role e operação.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Api/Users/`
- `Application/Users/`
- Testes de integração

## NÃO FAÇA

- Não permitir OPERATOR.
- Não adicionar rotas fora da especificação.
- Não retornar PasswordHash.
- Não excluir fisicamente sem decisão.

## REGRAS TÉCNICAS

- Users é exclusivo de ADMIN.
- Senhas são hasheadas com BCrypt.
- Soft delete segue DEC-004.

## CRITÉRIOS DE ACEITE

- [ ] ADMIN executa as cinco operações.
- [ ] OPERATOR recebe 403.
- [ ] Token inválido recebe 401.
- [ ] Senha nunca é retornada.
- [ ] DELETE executa soft delete.

## TESTES OBRIGATÓRIOS

- CRUD completo.
- Duplicate/invalid input.
- Not found.
- Unauthorized/forbidden.
- Soft delete.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Testes por endpoint.
- OpenAPI.
- Query de banco confirmando hash e IsDeleted.

## DEFINITION OF DONE

- [ ] Cinco endpoints implementados.
- [ ] RBAC testado.
- [ ] OpenAPI atualizado.

## PRÓXIMA TASK

TASK-029 — Criar telas de User Management.

# TASK-029 — Criar telas de User Management

STATUS: BLOCKED

EPIC: EPIC-003

FEATURE: FEAT-003.2

SIZE: M

DEPENDS ON: TASK-013, TASK-024, TASK-028, DEC-018

## CONTEXTO

ADMIN precisa de uma interface para listar e manter usuários. OPERATOR não deve acessar a área.

## OBJETIVO

Criar a página de listagem, filtros, paginação e links para create/edit/delete.

## O QUE VOCÊ DEVE FAZER

1. Criar rota de Users conforme DEC-018.
2. Consumir list endpoint.
3. Criar filtros e paginação.
4. Criar loading, empty e error states.
5. Adicionar links para create/edit/delete.
6. Impedir navegação de OPERATOR.
7. Criar testes de componente.

## ONDE TRABALHAR

- `frontend/src/pages/Users/`
- `frontend/src/components/`
- `frontend/src/hooks/`
- `frontend/src/validations/`

## NÃO FAÇA

- Não criar página de OPERATOR.
- Não fazer autorização apenas visual.
- Não duplicar validação que deve ser do backend.
- Não usar tabela sem estado de loading.

## REGRAS TÉCNICAS

- ADMIN é o único role autorizado.
- Paginação/filtro seguem DEC-002.
- Senhas não são exibidas.

## CRITÉRIOS DE ACEITE

- [ ] ADMIN vê a rota e a lista.
- [ ] Filtros alteram a query.
- [ ] Paginação não duplica registros.
- [ ] Loading/empty/error são visíveis.
- [ ] Hash não aparece.

## TESTES OBRIGATÓRIOS

- Renderização da lista.
- Filtro e paginação.
- Estados de API.
- Bloqueio para OPERATOR.

## COMO VALIDAR

```text
npm run lint
npm run build
```

## EVIDÊNCIAS

- Testes RTL.
- URL da rota.
- Captura dos estados, se usada.

## DEFINITION OF DONE

- [ ] Listagem funcional.
- [ ] Estados cobertos.
- [ ] Backend continua sendo autoridade.

## PRÓXIMA TASK

TASK-030 — Testar User Management.

# TASK-030 — Testar User Management

STATUS: BLOCKED

EPIC: EPIC-003

FEATURE: FEAT-003.3

SIZE: M

DEPENDS ON: TASK-014, TASK-015, TASK-028, TASK-029

## CONTEXTO

User Management reúne hash, roles, paginação, soft delete e UI. A cobertura deve evitar que uma feature seja considerada concluída apenas por build.

## OBJETIVO

Cobrir User Management nas camadas de domínio, API, autorização e frontend.

## O QUE VOCÊ DEVE FAZER

1. Criar testes de DTO/validator.
2. Criar testes de create/read/update/delete.
3. Criar matriz ADMIN/OPERATOR.
4. Criar testes de duplicate, invalid e not found.
5. Criar testes de soft delete.
6. Criar testes RTL da listagem e formulário.
7. Registrar relatório.

## ONDE TRABALHAR

- `backend/tests/Users/`
- `frontend/src/pages/Users/**/*.test.*`
- Fixtures de teste

## NÃO FAÇA

- Não testar apenas snapshot.
- Não considerar build como teste.
- Não usar dados de produção.

## REGRAS TÉCNICAS

- Senha deve estar hasheada.
- OPERATOR não acessa Users.
- Soft delete é o comportamento aprovado.

## CRITÉRIOS DE ACEITE

- [ ] Happy path completo coberto.
- [ ] Erros relevantes cobertos.
- [ ] RBAC coberto.
- [ ] Soft delete e hash verificados.
- [ ] Estados UI cobertos.

## TESTES OBRIGATÓRIOS

- Unitários de validator.
- Integração de endpoints.
- RTL de lista/formulário.
- E2E de Users quando TASK-015 estiver pronta.

## COMO VALIDAR

```text
dotnet test EnterpriseManagement.slnx
npm test
```

## EVIDÊNCIAS

- Relatório de testes.
- Matriz de cenários.
- Requests/respostas sanitizadas.

## DEFINITION OF DONE

- [ ] User Management testada.
- [ ] Falhas são detectáveis.
- [ ] Evidência registrada.

## PRÓXIMA TASK

TASK-031 — Criar DTOs e validação de Client.

# TASK-031 — Criar DTOs e validação de Client

STATUS: BLOCKED

EPIC: EPIC-004

FEATURE: FEAT-004.1

SIZE: S

DEPENDS ON: TASK-004, TASK-007, DEC-003, DEC-006

## CONTEXTO

Client possui Type, Document, Name, Email e Phone. PF/PJ é confirmado, mas formato, required condicional, normalização e unicidade do documento são pendentes.

## OBJETIVO

Definir DTOs e validações de Client sem inventar regras de CPF/CNPJ.

## O QUE VOCÊ DEVE FAZER

1. Criar DTOs de create/update/read/list.
2. Validar tipo PF/PJ.
3. Aplicar regras de document conforme DEC-006.
4. Validar Name, Email e Phone conforme DEC-003.
5. Definir resposta sem campos não existentes.
6. Criar testes de validator.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Application/Clients/`
- DTOs, validators e testes

## NÃO FAÇA

- Não adicionar endereço ou score.
- Não inventar algoritmo de CPF/CNPJ.
- Não implementar endpoint.

## REGRAS TÉCNICAS

- PF/PJ é obrigatório.
- Document segue decisão DEC-006.
- Unicidade é condicional à aprovação.

## CRITÉRIOS DE ACEITE

- [ ] PF/PJ possuem contrato.
- [ ] Documento inválido é rejeitado conforme decisão.
- [ ] Duplicidade tem tratamento definido.
- [ ] Campos condicionais são validados.

## TESTES OBRIGATÓRIOS

- PF válido.
- PJ válido.
- Tipo inválido.
- Documento vazio/inválido.
- Duplicate conforme decisão.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- DTOs.
- Testes de validação.
- DEC-006 aplicada.

## DEFINITION OF DONE

- [ ] Contrato Client aprovado.
- [ ] Validação testada.
- [ ] Nenhuma regra inventada.

## PRÓXIMA TASK

TASK-032 — Implementar service de Client.

# TASK-032 — Implementar service de Client

STATUS: BLOCKED

EPIC: EPIC-004

FEATURE: FEAT-004.1

SIZE: M

DEPENDS ON: TASK-010, TASK-031, DEC-002, DEC-004, DEC-005, DEC-006

## CONTEXTO

Clients precisam de CRUD, consulta paginada e filtro, com relação com Orders. O delete behavior com pedidos é DEC-005.

## OBJETIVO

Implementar a camada de aplicação de Client com persistência, filtros, mapping e soft delete aprovado.

## O QUE VOCÊ DEVE FAZER

1. Implementar create/update.
2. Implementar list/get.
3. Aplicar paginação e filtros de DEC-002.
4. Aplicar query filter de soft delete.
5. Respeitar delete behavior com Order.
6. Criar testes de integração.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Application/Clients/`
- `backend/src/EnterpriseManagement.Infrastructure/Persistence/Clients/`
- Testes

## NÃO FAÇA

- Não implementar UI.
- Não implementar stock.
- Não excluir fisicamente.
- Não apagar Order por cascade não aprovado.

## REGRAS TÉCNICAS

- PF/PJ e documento seguem DEC-006.
- Client possui N Orders.
- Soft delete e delete behavior são decisões explícitas.

## CRITÉRIOS DE ACEITE

- [ ] CRUD persiste campos válidos.
- [ ] Lista e filtros seguem contrato.
- [ ] Registro deletado não aparece em consulta normal.
- [ ] Pedido relacionado não é corrompido.

## TESTES OBRIGATÓRIOS

- Create/read/update/delete lógico.
- Filtros e paginação.
- Duplicate.
- Not found.
- Cliente com Order e delete behavior.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Testes de integração.
- Query de banco.
- ADR de delete behavior.

## DEFINITION OF DONE

- [ ] Service Client testado.
- [ ] Relacionamento preservado.
- [ ] Soft delete comprovado.

## PRÓXIMA TASK

TASK-033 — Expor CRUD de Clients.

# TASK-033 — Expor CRUD de Clients

STATUS: BLOCKED

EPIC: EPIC-004

FEATURE: FEAT-004.1

SIZE: M

DEPENDS ON: TASK-022, TASK-031, TASK-032

## CONTEXTO

As cinco rotas de Clients devem ser acessíveis a ADMIN e OPERATOR. O service existe somente após TASK-032.

## OBJETIVO

Expor list, detail, create, update e delete com contrato, validação e policy.

## O QUE VOCÊ DEVE FAZER

1. Implementar `GET /api/clients`.
2. Implementar `GET /api/clients/{id}`.
3. Implementar `POST /api/clients`.
4. Implementar `PUT /api/clients/{id}`.
5. Implementar `DELETE /api/clients/{id}`.
6. Aplicar policies e erros comuns.
7. Adicionar OpenAPI e testes.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Api/Clients/`
- `Application/Clients/`
- Testes de integração

## NÃO FAÇA

- Não adicionar rotas extras.
- Não remover policy de OPERATOR.
- Não implement delete behavior fora do service.

## REGRAS TÉCNICAS

- ADMIN e OPERATOR têm acesso.
- Validação de documento segue DEC-006.
- Erros/paginação seguem TASK-004.

## CRITÉRIOS DE ACEITE

- [ ] Cinco endpoints funcionam.
- [ ] Ambos os roles autorizados acessam.
- [ ] Anônimo recebe 401.
- [ ] Validação e delete behavior são respeitados.
- [ ] OpenAPI documenta o contrato.

## TESTES OBRIGATÓRIOS

- CRUD completo.
- Role matrix.
- Invalid/duplicate/not found.
- Soft delete.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Testes.
- OpenAPI.
- Requests sanitizadas.

## DEFINITION OF DONE

- [ ] CRUD de Clients exposto.
- [ ] RBAC aplicado.
- [ ] Contrato documentado.

## PRÓXIMA TASK

TASK-034 — Criar telas de Client Management.

# TASK-034 — Criar telas de Client Management

STATUS: BLOCKED

EPIC: EPIC-004

FEATURE: FEAT-004.2

SIZE: M

DEPENDS ON: TASK-013, TASK-033, DEC-018

## CONTEXTO

ADMIN e OPERATOR precisam cadastrar e consultar clientes PF/PJ. O caminho das rotas e a UX de formulário são DEC-018.

## OBJETIVO

Criar listagem, filtros, paginação, detail e formulário condicional PF/PJ.

## O QUE VOCÊ DEVE FAZER

1. Criar rota de Clients.
2. Consumir list endpoint.
3. Criar filtro de tipo e documento conforme aprovado.
4. Criar paginação.
5. Criar form PF/PJ.
6. Criar detail e delete confirmation.
7. Tratar loading, empty, error e duplicate.
8. Criar testes de componente.

## ONDE TRABALHAR

- `frontend/src/pages/Clients/`
- `frontend/src/components/forms/`
- `frontend/src/hooks/`
- `frontend/src/validations/`

## NÃO FAÇA

- Não adicionar cadastro de crédito.
- Não exibir documento em formato não aprovado.
- Não esconder erros de duplicate.

## REGRAS TÉCNICAS

- ADMIN e OPERATOR têm acesso.
- PF/PJ e documento seguem DEC-006.
- Soft delete segue TASK-032.

## CRITÉRIOS DE ACEITE

- [ ] Lista e filtros funcionam.
- [ ] PF/PJ alteram campos conforme decisão.
- [ ] Duplicate/invalid são exibidos.
- [ ] Estados de loading/empty/error existem.
- [ ] Delete solicita confirmação.

## TESTES OBRIGATÓRIOS

- Renderização PF/PJ.
- Submit válido/inválido.
- Filtro/paginação.
- Erro e empty state.

## COMO VALIDAR

```text
npm run lint
npm run build
```

## EVIDÊNCIAS

- Testes RTL.
- Captura de PF/PJ.
- URL da rota.

## DEFINITION OF DONE

- [ ] Telas de Client prontas.
- [ ] Estados cobertos.
- [ ] Contrato da API consumida corretamente.

## PRÓXIMA TASK

TASK-035 — Testar Client Management.

# TASK-035 — Testar Client Management

STATUS: BLOCKED

EPIC: EPIC-004

FEATURE: FEAT-004.3

SIZE: M

DEPENDS ON: TASK-014, TASK-015, TASK-033, TASK-034

## CONTEXTO

Client Management precisa de cobertura de PF/PJ, documento, filtros, duplicidade, soft delete, RBAC e UI.

## OBJETIVO

Criar a suíte de testes backend e frontend da feature.

## O QUE VOCÊ DEVE FAZER

1. Testar validator de PF/PJ.
2. Testar persistência e duplicate.
3. Testar filtros/paginação.
4. Testar delete com Order.
5. Testar roles ADMIN/OPERATOR.
6. Testar form e estados UI.
7. Registrar resultados.

## ONDE TRABALHAR

- `backend/tests/Clients/`
- `frontend/src/pages/Clients/**/*.test.*`

## NÃO FAÇA

- Não testar somente status HTTP.
- Não usar banco de produção.
- Não ocultar delete behavior.

## REGRAS TÉCNICAS

- Regras de documento vêm de DEC-006.
- Pedido relacionado deve ser preservado conforme DEC-005.

## CRITÉRIOS DE ACEITE

- [ ] Happy path PF/PJ coberto.
- [ ] Invalid/duplicate/not found cobertos.
- [ ] Soft delete e relação com Order cobertos.
- [ ] Roles e estados UI cobertos.

## TESTES OBRIGATÓRIOS

- Unitários de validator.
- Integração de API.
- RTL de lista/form.
- E2E de Clients quando disponível.

## COMO VALIDAR

```text
dotnet test EnterpriseManagement.slnx
npm test
```

## EVIDÊNCIAS

- Relatório de testes.
- Matriz de cenários.
- Evidência de persistência.

## DEFINITION OF DONE

- [ ] Client Management testada.
- [ ] Evidência registrada.
- [ ] Regressões detectadas.

## PRÓXIMA TASK

TASK-036 — Criar DTOs e validação de Product.

# TASK-036 — Criar DTOs e validação de Product

STATUS: BLOCKED

EPIC: EPIC-005

FEATURE: FEAT-005.1

SIZE: S

DEPENDS ON: TASK-004, TASK-008, DEC-003, DEC-007

## CONTEXTO

Product possui Name, Description e Price, sem estoque. Precisão, moeda, required e unicidade de nome não estão definidas.

## OBJETIVO

Criar DTOs e validações do catálogo sem introduzir estoque.

## O QUE VOCÊ DEVE FAZER

1. Criar DTOs de create/update/read/list.
2. Validar Name e Description conforme DEC-003.
3. Validar Price conforme DEC-007.
4. Definir resposta sem estoque.
5. Criar testes de validator.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Application/Products/`
- Testes

## NÃO FAÇA

- Não criar Stock, Quantity ou SKU.
- Não inventar moeda/rounding.
- Não implementar endpoint.

## REGRAS TÉCNICAS

- Produtos não têm estoque no MVP.
- Price segue a decisão DEC-007.

## CRITÉRIOS DE ACEITE

- [ ] DTOs cobrem os campos definidos.
- [ ] Price inválido é rejeitado.
- [ ] Nenhum campo de estoque é criado.
- [ ] Testes unitários passam.

## TESTES OBRIGATÓRIOS

- Produto válido.
- Price inválido.
- Name/Description inválidos.
- Resposta sem estoque.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- DTOs.
- Testes.
- DEC-007 aplicada.

## DEFINITION OF DONE

- [ ] Contrato Product aprovado.
- [ ] Validação testada.
- [ ] Estoque permanece fora do escopo.

## PRÓXIMA TASK

TASK-037 — Implementar service de Product.

# TASK-037 — Implementar service de Product

STATUS: BLOCKED

EPIC: EPIC-005

FEATURE: FEAT-005.1

SIZE: M

DEPENDS ON: TASK-010, TASK-036, DEC-002, DEC-004, DEC-005, DEC-007

## CONTEXTO

Products serão referenciados por OrderItems. O que acontece com produto deletado que possui histórico é DEC-005.

## OBJETIVO

Implementar CRUD e consultas de Product com preço e soft delete aprovados.

## O QUE VOCÊ DEVE FAZER

1. Implementar create/update/get/list.
2. Aplicar paginação e filtros de DEC-002.
3. Persistir Price com precisão aprovada.
4. Aplicar soft delete.
5. Respeitar delete behavior com OrderItem.
6. Criar testes de integração.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Application/Products/`
- `backend/src/EnterpriseManagement.Infrastructure/Persistence/Products/`
- Testes

## NÃO FAÇA

- Não implementar estoque.
- Não apagar OrderItem por cascade não aprovado.
- Não implementar UI.

## REGRAS TÉCNICAS

- Produto não possui estoque no MVP.
- Delete behavior e soft delete são decisões explícitas.

## CRITÉRIOS DE ACEITE

- [ ] CRUD persiste campos válidos.
- [ ] Filtros/paginação seguem contrato.
- [ ] Preço mantém precisão aprovada.
- [ ] Produto com OrderItem não viola delete behavior.

## TESTES OBRIGATÓRIOS

- CRUD.
- Filtro/paginação.
- Preço inválido.
- Produto referenciado e delete.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Testes.
- Query de preço.
- ADR de delete behavior.

## DEFINITION OF DONE

- [ ] Service Product testado.
- [ ] Relação com OrderItem preservada.
- [ ] Estoque não implementado.

## PRÓXIMA TASK

TASK-038 — Expor CRUD de Products.

# TASK-038 — Expor CRUD de Products

STATUS: BLOCKED

EPIC: EPIC-005

FEATURE: FEAT-005.1

SIZE: M

DEPENDS ON: TASK-022, TASK-036, TASK-037

## CONTEXTO

As cinco rotas de Products devem estar disponíveis para ADMIN e OPERATOR, sem qualquer rota de estoque.

## OBJETIVO

Expor list, detail, create, update e delete de Product com RBAC e OpenAPI.

## O QUE VOCÊ DEVE FAZER

1. Implementar os cinco endpoints especificados.
2. Aplicar policies de role.
3. Usar DTOs e service da feature.
4. Mapear erros conforme TASK-004.
5. Documentar schemas no OpenAPI.
6. Criar testes de integração.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Api/Products/`
- `Application/Products/`
- Testes

## NÃO FAÇA

- Não criar endpoint de estoque.
- Não permitir roles não autorizados.
- Não alterar delete behavior no endpoint.

## REGRAS TÉCNICAS

- ADMIN e OPERATOR têm acesso.
- Estoque não faz parte do contrato.
- Preço segue DEC-007.

## CRITÉRIOS DE ACEITE

- [ ] Cinco endpoints funcionam.
- [ ] Ambos os roles acessam.
- [ ] Validação/preço são aplicados.
- [ ] Soft delete é respeitado.
- [ ] OpenAPI atualizado.

## TESTES OBRIGATÓRIOS

- CRUD.
- Roles.
- Invalid/duplicate/not found.
- Delete com OrderItem.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Testes por endpoint.
- OpenAPI.
- Requests sanitizadas.

## DEFINITION OF DONE

- [ ] CRUD de Product exposto.
- [ ] RBAC aplicado.
- [ ] Contrato documentado.

## PRÓXIMA TASK

TASK-039 — Criar telas de Product Management.

# TASK-039 — Criar telas de Product Management

STATUS: BLOCKED

EPIC: EPIC-005

FEATURE: FEAT-005.2

SIZE: M

DEPENDS ON: TASK-013, TASK-038, DEC-018

## CONTEXTO

ADMIN e OPERATOR precisam manter o catálogo sem estoque. Paths e estados de UI são DEC-018.

## OBJETIVO

Criar listagem, filtros, formulário, detalhe e exclusão lógica de Product.

## O QUE VOCÊ DEVE FAZER

1. Criar rota de Products.
2. Consumir list endpoint.
3. Criar filtro/paginação aprovados.
4. Criar form com Name, Description e Price.
5. Criar detail e delete confirmation.
6. Exibir loading, empty e error.
7. Criar testes de componente.

## ONDE TRABALHAR

- `frontend/src/pages/Products/`
- `frontend/src/components/forms/`
- `frontend/src/hooks/`
- `frontend/src/validations/`

## NÃO FAÇA

- Não criar tela de estoque.
- Não calcular preço no cliente como fonte final.
- Não esconder erros de validação.

## REGRAS TÉCNICAS

- Product não tem estoque no MVP.
- ADMIN e OPERATOR têm acesso.

## CRITÉRIOS DE ACEITE

- [ ] Lista e filtros funcionam.
- [ ] Form envia campos válidos.
- [ ] Preço inválido é exibido.
- [ ] Delete usa confirmação e soft delete.
- [ ] Estados de UI são testados.

## TESTES OBRIGATÓRIOS

- Lista/filtro/paginação.
- Form create/edit.
- Detail/delete.
- Loading/empty/error.

## COMO VALIDAR

```text
npm run lint
npm run build
```

## EVIDÊNCIAS

- Testes RTL.
- Captura do catálogo/formulário.
- URL da rota.

## DEFINITION OF DONE

- [ ] Telas de Product prontas.
- [ ] Estoque não aparece.
- [ ] Estados cobertos.

## PRÓXIMA TASK

TASK-040 — Testar Product Management.

# TASK-040 — Testar Product Management

STATUS: BLOCKED

EPIC: EPIC-005

FEATURE: FEAT-005.3

SIZE: M

DEPENDS ON: TASK-014, TASK-015, TASK-038, TASK-039

## CONTEXTO

Product Management precisa provar que o CRUD respeita preço, RBAC, delete behavior e a exclusão de estoque.

## OBJETIVO

Cobrir a feature em testes backend e frontend.

## O QUE VOCÊ DEVE FAZER

1. Testar validator e preço.
2. Testar CRUD e filtros.
3. Testar roles.
4. Testar produto referenciado por OrderItem.
5. Testar UI e estados.
6. Registrar evidências.

## ONDE TRABALHAR

- `backend/tests/Products/`
- `frontend/src/pages/Products/**/*.test.*`

## NÃO FAÇA

- Não testar estoque que não existe.
- Não considerar build como cobertura.
- Não usar produção.

## REGRAS TÉCNICAS

- Estoque está fora do MVP.
- Preço e delete behavior são decisões.

## CRITÉRIOS DE ACEITE

- [ ] Happy path e erros cobertos.
- [ ] RBAC coberto.
- [ ] Relação com OrderItem coberta.
- [ ] UI states cobertos.

## TESTES OBRIGATÓRIOS

- Unitários de validator.
- Integração de API.
- RTL de catálogo/form.
- E2E de Products quando disponível.

## COMO VALIDAR

```text
dotnet test EnterpriseManagement.slnx
npm test
```

## EVIDÊNCIAS

- Relatório de testes.
- Requests sanitizadas.
- Evidência de que não há estoque.

## DEFINITION OF DONE

- [ ] Product Management testada.
- [ ] Evidência registrada.
- [ ] Escopo sem estoque respeitado.

## PRÓXIMA TASK

TASK-041 — Definir contratos e regras de Order.

# TASK-041 — Definir contratos e regras de Order

STATUS: BLOCKED

EPIC: EPIC-006

FEATURE: FEAT-006.1

SIZE: M

DEPENDS ON: TASK-001, TASK-009, DEC-003, DEC-007, DEC-008, DEC-009

## CONTEXTO

A especificação define Order, OrderItem, TotalAmount, UnitPrice, TotalPrice e os status Pendente, Concluído e Cancelado. Não define transições, quantidade, snapshot, precisão ou edição.

## OBJETIVO

Fechar o contrato de Order e transformar as lacunas em decisões explícitas antes da implementação do service.

## O QUE VOCÊ DEVE FAZER

1. Criar DTOs de create, update, detail e list.
2. Documentar status e transições aprovadas.
3. Definir quantidade e validação de Product/Client.
4. Definir origem de UnitPrice e cálculo de TotalPrice/TotalAmount.
5. Definir snapshot, edição e concorrência.
6. Criar testes de contrato/validator.
7. Registrar todas as decisões aplicadas.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Application/Orders/Contracts/`
- `docs/decisions/`
- Testes de contrato

## NÃO FAÇA

- Não implementar persistência nesta task.
- Não adicionar status além dos três confirmados.
- Não inventar precisão, transições ou snapshot.

## REGRAS TÉCNICAS

- Status confirmados: Pendente, Concluído e Cancelado.
- Estoque não participa do pedido.
- Total e preço devem ter regra explícita.

## CRITÉRIOS DE ACEITE

- [ ] DTOs cobrem todos os campos definidos.
- [ ] Status permitidos estão documentados.
- [ ] Transições inválidas estão definidas.
- [ ] Cálculo de total está aprovado ou bloqueado explicitamente.
- [ ] Testes de contrato existem.

## TESTES OBRIGATÓRIOS

- Payload válido.
- Payload inválido.
- Status não permitido.
- Quantidade/preço conforme decisão.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Contrato.
- ADRs DEC-007/008/009.
- Testes de validator.

## DEFINITION OF DONE

- [ ] Contrato de Order aprovado ou bloqueado.
- [ ] Regras de status/total documentadas.
- [ ] Nenhuma lacuna escondida.

## PRÓXIMA TASK

TASK-042 — Implementar consultas de Order.

# TASK-042 — Implementar consultas de Order

STATUS: BLOCKED

EPIC: EPIC-006

FEATURE: FEAT-006.1

SIZE: M

DEPENDS ON: TASK-010, TASK-041, DEC-002, DEC-005

## CONTEXTO

Order possui relação com Client e OrderItems. As consultas precisam de paginação, filtros e relacionamentos, mas o delete behavior com Client/Product é DEC-005.

## OBJETIVO

Implementar services de listagem e detalhe de Order sem criar regras de escrita.

## O QUE VOCÊ DEVE FAZER

1. Implementar list com paginação/filtros aprovados.
2. Implementar detail com OrderItems e relacionamentos necessários.
3. Mapear para DTOs.
4. Respeitar soft delete e filtros de entidades relacionadas.
5. Implementar not found no contrato.
6. Criar testes de integração.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Application/Orders/`
- `backend/src/EnterpriseManagement.Infrastructure/Persistence/Orders/`
- Testes

## NÃO FAÇA

- Não criar Order.
- Não alterar status.
- Não apagar Client/Product.
- Não inventar filtros.

## REGRAS TÉCNICAS

- Order referencia Client e possui OrderItems.
- OrderItem referencia Product.
- Paginação/filtros seguem DEC-002.

## CRITÉRIOS DE ACEITE

- [ ] Listagem retorna página e metadados aprovados.
- [ ] Detail inclui itens conforme contrato.
- [ ] Filtros funcionam.
- [ ] ID inexistente é tratado.
- [ ] Relacionamentos não são corrompidos.

## TESTES OBRIGATÓRIOS

- Lista vazia/página/filtro.
- Detail com e sem itens.
- ID inexistente.
- Cliente/Product deletado conforme decisão.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Testes de integração.
- Queries de banco.
- Contrato de paginação.

## DEFINITION OF DONE

- [ ] Consultas de Order testadas.
- [ ] Relacionamentos preservados.
- [ ] Sem escrita implementada.

## PRÓXIMA TASK

TASK-043 — Implementar criação transacional e total.

# TASK-043 — Implementar criação transacional e total

STATUS: BLOCKED

EPIC: EPIC-006

FEATURE: FEAT-006.1

SIZE: M

DEPENDS ON: TASK-041, TASK-042, DEC-007, DEC-009

## CONTEXTO

Order e OrderItem precisam ser persistidos de forma consistente. A especificação não define a fórmula, snapshot, quantidade, atomicidade ou idempotência; esses pontos são decisões.

## OBJETIVO

Implementar a criação de pedido e itens usando a regra de total aprovada, sem persistência parcial.

## O QUE VOCÊ DEVE FAZER

1. Validar Client, Product, Quantity e itens.
2. Resolver UnitPrice conforme DEC-009.
3. Calcular TotalPrice e TotalAmount conforme DEC-007.
4. Persistir Order e OrderItems na transação aprovada.
5. Implementar rollback em erro.
6. Tratar duplicidade/idempotência se aprovada.
7. Criar testes de sucesso, rollback e valores.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Application/Orders/CreateOrder/`
- `backend/src/EnterpriseManagement.Infrastructure/Persistence/Orders/`
- Testes de integração

## NÃO FAÇA

- Não inventar fórmula de total.
- Não calcular no frontend como autoridade.
- Não criar estoque ou pagamento.
- Não persistir pedido parcial.

## REGRAS TÉCNICAS

- `TotalAmount` e valores de item pertencem ao Order.
- Relação Client/Product é obrigatória.
- Transação é implementação técnica e deve ser justificada pela decisão DEC-009.

## CRITÉRIOS DE ACEITE

- [ ] Pedido e itens são persistidos conforme decisão.
- [ ] Total corresponde à fórmula aprovada.
- [ ] Falha não deixa registro parcial.
- [ ] Quantidade e preço inválidos são rejeitados.
- [ ] Testes de rollback existem.

## TESTES OBRIGATÓRIOS

- Happy path.
- Produto/Client inexistente.
- Quantidade inválida.
- Erro no segundo item.
- Precisão/arredondamento.
- Reenvio/idempotência, se aprovado.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Testes transacionais.
- Valores persistidos.
- ADR de total/atomicidade.

## DEFINITION OF DONE

- [ ] Criação de Order testada.
- [ ] Total e rollback comprovados.
- [ ] Regra aprovada.

## PRÓXIMA TASK

TASK-044 — Implementar transições de status.

# TASK-044 — Implementar transições de status

STATUS: BLOCKED

EPIC: EPIC-006

FEATURE: FEAT-006.1

SIZE: M

DEPENDS ON: TASK-041, TASK-042, DEC-008

## CONTEXTO

A especificação confirma três status, mas não define se as transições são reversíveis ou terminais.

## OBJETIVO

Aplicar uma matriz de transições explícita e impedir alterações inválidas.

## O QUE VOCÊ DEVE FAZER

1. Documentar matriz de transições.
2. Implementar regra no Domain/Application.
3. Validar status atual e novo status.
4. Persistir somente transições permitidas.
5. Respeitar concorrência aprovada.
6. Criar testes de todas as combinações.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Domain/Orders/`
- `backend/src/EnterpriseManagement.Application/Orders/ChangeStatus/`
- Testes

## NÃO FAÇA

- Não adicionar status.
- Não permitir mudança arbitrária.
- Não implementar endpoint nesta task.
- Não inventar transições.

## REGRAS TÉCNICAS

- Status válidos são Pendente, Concluído e Cancelado.
- Transições são DEC-008.
- OPERATOR e ADMIN podem acessar a área, conforme matrix.

## CRITÉRIOS DE ACEITE

- [ ] Transições permitidas são persistidas.
- [ ] Transições proibidas falham sem alteração.
- [ ] Pedido inexistente é tratado.
- [ ] Concorrência não sobrescreve estado indevidamente.

## TESTES OBRIGATÓRIOS

- Matriz completa de status.
- Repetição da mesma transição.
- Transição inválida.
- Not found.
- Roles.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Matriz de transições.
- Testes de persistência.
- ADR DEC-008.

## DEFINITION OF DONE

- [ ] Regra de status implementada.
- [ ] Matriz testada.
- [ ] Nenhum status inventado.

## PRÓXIMA TASK

TASK-045 — Expor APIs de Orders.

# TASK-045 — Expor APIs de Orders

STATUS: BLOCKED

EPIC: EPIC-006

FEATURE: FEAT-006.2

SIZE: M

DEPENDS ON: TASK-022, TASK-041, TASK-042, TASK-043, TASK-044, DEC-001, DEC-002

## CONTEXTO

A especificação define cinco rotas de Order e não define DELETE. Os endpoints devem usar os services de consulta, criação e status.

## OBJETIVO

Implementar list, detail, create, update e PATCH status com RBAC e OpenAPI.

## O QUE VOCÊ DEVE FAZER

1. Implementar `GET /api/orders`.
2. Implementar `GET /api/orders/{id}`.
3. Implementar `POST /api/orders`.
4. Implementar `PUT /api/orders/{id}` conforme semântica aprovada.
5. Implementar `PATCH /api/orders/{id}/status`.
6. Aplicar policies ADMIN/OPERATOR.
7. Adicionar OpenAPI e testes.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Api/Orders/`
- `Application/Orders/`
- Testes de integração

## NÃO FAÇA

- Não criar DELETE de Order.
- Não permitir status fora da matriz.
- Não calcular total no endpoint.
- Não adicionar rota extra.

## REGRAS TÉCNICAS

- ADMIN e OPERATOR têm acesso a Orders.
- Status e total seguem DEC-007/008/009.
- Erros e paginação seguem TASK-004.

## CRITÉRIOS DE ACEITE

- [ ] Cinco endpoints funcionam.
- [ ] Roles são aplicados.
- [ ] Criação e status usam services transacionais.
- [ ] Update não viola estado.
- [ ] OpenAPI está completo.

## TESTES OBRIGATÓRIOS

- List/detail/create/update/PATCH.
- Unauthorized/forbidden.
- Not found/invalid state.
- Rollback e total.
- Filtros/paginação.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Testes por rota.
- OpenAPI.
- Requests/respostas sanitizadas.

## DEFINITION OF DONE

- [ ] APIs de Order implementadas.
- [ ] RBAC testado.
- [ ] Contrato documentado.

## PRÓXIMA TASK

TASK-046 — Criar telas de Order Management.

# TASK-046 — Criar telas de Order Management

STATUS: BLOCKED

EPIC: EPIC-006

FEATURE: FEAT-006.3

SIZE: M

DEPENDS ON: TASK-013, TASK-045, DEC-018

## CONTEXTO

ADMIN e OPERATOR precisam consultar e manipular pedidos. A UI deve enviar dados permitidos e não substituir o cálculo do backend.

## OBJETIVO

Criar lista, detalhe, criação e mudança de status de Order.

## O QUE VOCÊ DEVE FAZER

1. Criar rota de Orders.
2. Consumir list/detail.
3. Criar filtros/paginação aprovados.
4. Criar form de Client e itens.
5. Exibir total retornado pelo backend.
6. Criar ação PATCH de status.
7. Tratar loading, empty, error e transição inválida.
8. Criar testes de componente.

## ONDE TRABALHAR

- `frontend/src/pages/Orders/`
- `frontend/src/components/orders/`
- `frontend/src/hooks/`
- `frontend/src/validations/`

## NÃO FAÇA

- Não criar DELETE de Order.
- Não calcular total autoritativo no cliente.
- Não inventar transições.
- Não esconder erro de rollback.

## REGRAS TÉCNICAS

- ADMIN e OPERATOR têm acesso.
- Status e total vêm do backend.
- Rotas seguem DEC-018.

## CRITÉRIOS DE ACEITE

- [ ] Lista e detail exibem dados do contrato.
- [ ] Form valida itens e cliente.
- [ ] Total exibido não substitui backend.
- [ ] Status inválido não é tratado como sucesso.
- [ ] Estados de UI são testados.

## TESTES OBRIGATÓRIOS

- Lista/filtro/paginação.
- Criação válida/inválida.
- Detalhe.
- Status permitido e proibido.
- Erro de API/rollback.

## COMO VALIDAR

```text
npm run lint
npm run build
```

## EVIDÊNCIAS

- Testes RTL.
- Capturas dos fluxos.
- URL da rota.

## DEFINITION OF DONE

- [ ] Telas de Order prontas.
- [ ] Backend é autoridade.
- [ ] Estados cobertos.

## PRÓXIMA TASK

TASK-047 — Testar Order Management.

# TASK-047 — Testar Order Management

STATUS: BLOCKED

EPIC: EPIC-006

FEATURE: FEAT-006.4

SIZE: L

DEPENDS ON: TASK-014, TASK-015, TASK-043, TASK-044, TASK-045, TASK-046

## CONTEXTO

Order é o fluxo com maior risco de consistência: itens, total, transação, status, concorrência e UI precisam ser validados em conjunto.

## OBJETIVO

Cobrir Order Management de ponta a ponta nas camadas relevantes.

## O QUE VOCÊ DEVE FAZER

1. Testar contratos e cálculos.
2. Testar create transacional e rollback.
3. Testar todos os status e transições.
4. Testar list/detail/filtros.
5. Testar roles e not found.
6. Testar concorrência.
7. Testar UI e E2E.

## ONDE TRABALHAR

- `backend/tests/Orders/`
- `frontend/src/pages/Orders/**/*.test.*`
- `frontend/e2e/orders` após TASK-015

## NÃO FAÇA

- Não testar apenas status HTTP.
- Não aceitar snapshot de teste como única evidência.
- Não usar dados de produção.
- Não testar regras não aprovadas.

## REGRAS TÉCNICAS

- Atomicidade e total seguem DEC-007/009.
- Transições seguem DEC-008.
- ADMIN/OPERATOR têm acesso.

## CRITÉRIOS DE ACEITE

- [ ] Happy path grava Order e itens.
- [ ] Falha no meio causa rollback.
- [ ] Total e UnitPrice/TotalPrice estão corretos.
- [ ] Matriz de status está coberta.
- [ ] Concorrência e roles estão cobertos.
- [ ] UI e E2E relevantes têm evidência.

## TESTES OBRIGATÓRIOS

- Unitários de status/total.
- Integração de cada endpoint.
- RTL de lista/form/status.
- E2E do fluxo Order.

## COMO VALIDAR

```text
dotnet test EnterpriseManagement.slnx
npm test
npx playwright test
```

## EVIDÊNCIAS

- Relatório backend/frontend/E2E.
- Requests sanitizadas.
- Valores de banco.
- Traces/screenshots de falha, quando existirem.

## DEFINITION OF DONE

- [ ] Order Management testada.
- [ ] Rollback e transições comprovados.
- [ ] Evidência completa.

## PRÓXIMA TASK

TASK-048 — Definir métricas do Dashboard.

# TASK-048 — Definir métricas do Dashboard

STATUS: READY

EPIC: EPIC-007

FEATURE: FEAT-007.1

SIZE: S

DEPENDS ON: TASK-001, DEC-007, DEC-017

## CONTEXTO

A especificação confirma dashboard com cards/indicadores, mas não define quais métricas, período ou estados entram nos cálculos.

## OBJETIVO

Definir cards, cálculo, período, fonte de dados e estado vazio antes da query.

## O QUE VOCÊ DEVE FAZER

1. Listar cards candidatos para aprovação.
2. Definir fórmula e fonte de cada card.
3. Definir período e filtro.
4. Definir inclusão de cancelados/concluídos.
5. Definir precisão dos valores.
6. Criar fixtures de cálculo.
7. Registrar DEC-017.

## ONDE TRABALHAR

- `docs/decisions/`
- Contratos de Application/Dashboard
- Testes de fixtures

## NÃO FAÇA

- Não criar gráficos ou BI.
- Não inventar cards.
- Não implementar query antes da aprovação.
- Não misturar métrica não confirmada.

## REGRAS TÉCNICAS

- Dashboard é apenas indicador no MVP.
- ADMIN e OPERATOR podem acessar.
- Valores monetários seguem DEC-007.

## CRITÉRIOS DE ACEITE

- [ ] Cada card possui definição aprovada.
- [ ] Cálculo é reproduzível em fixture.
- [ ] Período e estados estão explícitos.
- [ ] Response e empty state documentados.

## TESTES OBRIGATÓRIOS

- Teste de fórmula de cada card.
- Teste de período.
- Teste de estado vazio.

## COMO VALIDAR

```text
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- ADR DEC-017.
- Tabela de métricas.
- Fixtures de teste.

## DEFINITION OF DONE

- [ ] Métricas aprovadas.
- [ ] Cálculos definidos.
- [ ] Nenhum card inventado.

## PRÓXIMA TASK

TASK-049 — Implementar query e endpoint do Dashboard.

# TASK-049 — Implementar query e endpoint do Dashboard

STATUS: BLOCKED

EPIC: EPIC-007

FEATURE: FEAT-007.1

SIZE: M

DEPENDS ON: TASK-022, TASK-042, TASK-048

## CONTEXTO

O endpoint `GET /api/dashboard` está definido, mas os cards e filtros não. A query precisa respeitar os dados e o RBAC.

## OBJETIVO

Implementar a query de indicadores e o endpoint protegido.

## O QUE VOCÊ DEVE FAZER

1. Criar DTO de response conforme DEC-017.
2. Implementar agregações aprovadas.
3. Aplicar período/filtros.
4. Respeitar soft delete.
5. Expor endpoint para ADMIN/OPERATOR.
6. Tratar banco vazio e erros.
7. Criar testes de integração.

## ONDE TRABALHAR

- `backend/src/EnterpriseManagement.Application/Dashboard/`
- `backend/src/EnterpriseManagement.Infrastructure/Queries/`
- `backend/src/EnterpriseManagement.Api/Dashboard/`
- Testes

## NÃO FAÇA

- Não criar escrita no dashboard.
- Não adicionar gráficos.
- Não inventar métrica.
- Não ignorar role.

## REGRAS TÉCNICAS

- ADMIN e OPERATOR têm acesso.
- Valores seguem DEC-007.
- Cards seguem DEC-017.

## CRITÉRIOS DE ACEITE

- [ ] Cards retornados correspondem à decisão.
- [ ] Valores batem com fixtures.
- [ ] Banco vazio retorna empty state.
- [ ] Anônimo/role indevido é rejeitado.
- [ ] Endpoint documentado.

## TESTES OBRIGATÓRIOS

- Banco populado.
- Banco vazio.
- Período/filtro.
- ADMIN/OPERATOR/anônimo.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Testes de agregação.
- Response sanitizada.
- OpenAPI.

## DEFINITION OF DONE

- [ ] Endpoint Dashboard implementado.
- [ ] Cálculos testados.
- [ ] RBAC aplicado.

## PRÓXIMA TASK

TASK-050 — Criar tela de Dashboard.

# TASK-050 — Criar tela de Dashboard

STATUS: BLOCKED

EPIC: EPIC-007

FEATURE: FEAT-007.2

SIZE: M

DEPENDS ON: TASK-013, TASK-049, DEC-018

## CONTEXTO

Não existe página de Dashboard. O path e o layout são DEC-018, e os cards são DEC-017.

## OBJETIVO

Exibir os indicadores autorizados com estados de loading, vazio, erro e retry.

## O QUE VOCÊ DEVE FAZER

1. Criar rota protegida de Dashboard.
2. Consumir `GET /api/dashboard`.
3. Renderizar cards conforme mapping.
4. Formatar números e moeda.
5. Implementar loading, empty, error e retry.
6. Impedir acesso de OPERATOR a Users.
7. Criar testes de componente.

## ONDE TRABALHAR

- `frontend/src/pages/Dashboard/`
- `frontend/src/components/dashboard/`
- `frontend/src/hooks/`
- `frontend/src/types/`

## NÃO FAÇA

- Não criar edição de dados.
- Não adicionar gráfico avançado.
- Não recalcular métricas no cliente.
- Não acessar Users no menu do OPERATOR.

## REGRAS TÉCNICAS

- ADMIN e OPERATOR podem acessar.
- Cards e cálculos vêm da API.
- Estados de erro devem ser compreensíveis.

## CRITÉRIOS DE ACEITE

- [ ] Cards correspondem um a um ao response.
- [ ] Loading/empty/error/retry existem.
- [ ] Valores não são convertidos incorretamente.
- [ ] Rota protegida e role menus corretos.
- [ ] Testes cobrem estados.

## TESTES OBRIGATÓRIOS

- Cards com dados.
- Loading.
- Empty.
- Error/retry.
- Role/session.

## COMO VALIDAR

```text
npm run lint
npm run build
```

## EVIDÊNCIAS

- Testes RTL.
- Capturas dos estados.
- URL da rota.

## DEFINITION OF DONE

- [ ] Dashboard funcional.
- [ ] Estados testados.
- [ ] Indicadores consistentes.

## PRÓXIMA TASK

TASK-051 — Testar Dashboard.

# TASK-051 — Testar Dashboard

STATUS: BLOCKED

EPIC: EPIC-007

FEATURE: FEAT-007.3

SIZE: M

DEPENDS ON: TASK-014, TASK-015, TASK-049, TASK-050

## CONTEXTO

Dashboard precisa de testes de cálculo, endpoint, autorização e estados de UI. Sem eles, cards podem divergir da fonte de dados.

## OBJETIVO

Cobrir a feature Dashboard em backend e frontend.

## O QUE VOCÊ DEVE FAZER

1. Testar fórmula de cada card.
2. Testar período e filtros.
3. Testar banco vazio.
4. Testar roles.
5. Testar cards e retry.
6. Criar E2E quando a infraestrutura existir.
7. Registrar resultados.

## ONDE TRABALHAR

- `backend/tests/Dashboard/`
- `frontend/src/pages/Dashboard/**/*.test.*`
- `frontend/e2e/dashboard` após TASK-015

## NÃO FAÇA

- Não testar métricas não aprovadas.
- Não considerar build como teste.
- Não esconder divergência de cálculo.

## REGRAS TÉCNICAS

- Cards seguem DEC-017.
- Valores seguem DEC-007.
- ADMIN/OPERATOR têm acesso.

## CRITÉRIOS DE ACEITE

- [ ] Cada card tem teste de cálculo.
- [ ] Empty/error/loading/retry cobertos.
- [ ] Roles cobertos.
- [ ] E2E ou limitação reportada.

## TESTES OBRIGATÓRIOS

- Integração de agregação.
- RTL de cards.
- E2E do Dashboard.

## COMO VALIDAR

```text
dotnet test EnterpriseManagement.slnx
npm test
npx playwright test
```

## EVIDÊNCIAS

- Relatório de testes.
- Fixtures de cálculo.
- Capturas/requests sanitizadas.

## DEFINITION OF DONE

- [ ] Dashboard testado.
- [ ] Evidência registrada.
- [ ] Divergências detectáveis.

## PRÓXIMA TASK

TASK-052 — Consolidar testes backend por camadas.

# TASK-052 — Consolidar testes backend por camadas

STATUS: BLOCKED

EPIC: EPIC-008

FEATURE: FEAT-008.1

SIZE: M

DEPENDS ON: TASK-014, TASK-025, TASK-030, TASK-035, TASK-040, TASK-047, TASK-051

## CONTEXTO

As features podem criar testes locais, mas o projeto ainda precisa de uma suíte backend consolidada e executável.

## OBJETIVO

Garantir que testes unitários e de integração cubram Domain, Application, Infrastructure e Api.

## O QUE VOCÊ DEVE FAZER

1. Revisar testes criados pelas features.
2. Remover fixtures duplicadas ou inadequadas.
3. Criar comandos de teste por camada, se necessário.
4. Garantir isolamento do banco.
5. Executar a suíte completa.
6. Registrar resultado e limitações.

## ONDE TRABALHAR

- `backend/tests/`
- `backend/EnterpriseManagement.slnx`
- Scripts de teste

## NÃO FAÇA

- Não transformar ausência de teste em PASS.
- Não usar produção.
- Não apagar dados.
- Não inventar threshold de cobertura.

## REGRAS TÉCNICAS

- xUnit e integração são especificados.
- Banco de teste é DEC-020.
- Testes devem ser reproduzíveis.

## CRITÉRIOS DE ACEITE

- [ ] `dotnet test` executa todas as suites.
- [ ] Testes de domínio/API/infraestrutura estão presentes.
- [ ] Banco de teste é isolado.
- [ ] Resultado e limitações são registrados.

## TESTES OBRIGATÓRIOS

- Suite completa do solution.
- Teste de isolamento.
- Testes de regressão das features.

## COMO VALIDAR

```text
dotnet test EnterpriseManagement.slnx
```

## EVIDÊNCIAS

- Relatório de testes.
- Comando e versão do SDK.
- Banco utilizado.

## DEFINITION OF DONE

- [ ] Suíte backend executável.
- [ ] Isolamento garantido.
- [ ] Evidência disponível.

## PRÓXIMA TASK

TASK-053 — Consolidar testes frontend.

# TASK-053 — Consolidar testes frontend

STATUS: BLOCKED

EPIC: EPIC-008

FEATURE: FEAT-008.2

SIZE: M

DEPENDS ON: TASK-015, TASK-023, TASK-029, TASK-034, TASK-039, TASK-046, TASK-050

## CONTEXTO

Vitest e RTL foram configurados como tarefa de foundation, mas as telas e fluxos ainda não possuem uma suíte consolidada.

## OBJETIVO

Cobrir services, hooks, forms, routes, estados e integração dos componentes do frontend.

## O QUE VOCÊ DEVE FAZER

1. Criar helpers de render e mocks.
2. Testar API client e AuthContext.
3. Testar Login e ProtectedRoute.
4. Testar pages de Users, Clients, Products, Orders e Dashboard.
5. Testar loading, empty, error e success.
6. Executar lint, testes e build.
7. Registrar relatório.

## ONDE TRABALHAR

- `frontend/src/**/*.test.*`
- `frontend/vitest.config.*`
- `frontend/package.json`

## NÃO FAÇA

- Não usar API real quando mock basta.
- Não depender de timing inseguro.
- Não considerar snapshot como única evidência.

## REGRAS TÉCNICAS

- Vitest e React Testing Library são especificações.
- API mock deve seguir TASK-004.
- Estados de loading/error são obrigatórios no backlog.

## CRITÉRIOS DE ACEITE

- [ ] `npm test` executa a suíte.
- [ ] Auth e pages possuem testes relevantes.
- [ ] Estados de UI estão cobertos.
- [ ] Lint e build passam.

## TESTES OBRIGATÓRIOS

- API client.
- AuthContext.
- ProtectedRoute.
- Cada página principal.
- Estados de API.

## COMO VALIDAR

```text
npm test
npm run lint
npm run build
```

## EVIDÊNCIAS

- Relatório de testes.
- Log de lint/build.
- Lista de cenários.

## DEFINITION OF DONE

- [ ] Suíte frontend executável.
- [ ] Comandos passam.
- [ ] Evidência registrada.

## PRÓXIMA TASK

TASK-054 — Automatizar E2E dos seis fluxos.

# TASK-054 — Automatizar E2E dos seis fluxos

STATUS: BLOCKED

EPIC: EPIC-008

FEATURE: FEAT-008.3

SIZE: L

DEPENDS ON: TASK-015, TASK-025, TASK-030, TASK-035, TASK-040, TASK-047, TASK-051

## CONTEXTO

A especificação exige Playwright e seis fluxos E2E: Login, Users, Clients, Products, Orders e Dashboard. Nenhum cenário existe.

## OBJETIVO

Criar uma suíte E2E integrada, repetível e isolada para os seis fluxos.

## O QUE VOCÊ DEVE FAZER

1. Configurar webServer e banco de teste.
2. Criar dados de teste seedados.
3. Criar fluxo de Login válido/inválido.
4. Criar fluxo de Users para ADMIN.
5. Criar fluxo de Clients.
6. Criar fluxo de Products.
7. Criar fluxo de Orders.
8. Criar fluxo de Dashboard.
9. Configurar traces/screenshots em falhas.
10. Executar a suíte.

## ONDE TRABALHAR

- `frontend/e2e/`
- `frontend/playwright.config.*`
- Scripts de seed/test environment

## NÃO FAÇA

- Não usar dados de produção.
- Não criar cenários de negócio não especificados.
- Não considerar teste manual como E2E.
- Não esconder_flaky tests.

## REGRAS TÉCNICAS

- ADMIN acessa todas as áreas.
- OPERATOR acessa Clients, Products, Orders e Dashboard.
- Playwright é a ferramenta especificada.

## CRITÉRIOS DE ACEITE

- [ ] Os seis fluxos possuem cenário.
- [ ] Happy path e erros relevantes são cobertos.
- [ ] Dados são isolados.
- [ ] `npx playwright test` executa ou tem limitação registrada.

## TESTES OBRIGATÓRIOS

- Login inválido.
- Users forbidden para OPERATOR.
- CRUD de Clients/Products.
- Order create/status.
- Dashboard carregado.

## COMO VALIDAR

```text
npx playwright test
```

## EVIDÊNCIAS

- HTML report.
- Screenshots/traces.
- Log de webServer/banco.
- Resultado por fluxo.

## DEFINITION OF DONE

- [ ] Seis fluxos cobertos.
- [ ] Ambiente reproduzível.
- [ ] Falhas reportadas honestamente.

## PRÓXIMA TASK

TASK-055 — Executar hardening e auditoria.

# TASK-055 — Executar hardening e auditoria

STATUS: BLOCKED

EPIC: EPIC-008

FEATURE: FEAT-008.4

SIZE: M

DEPENDS ON: TASK-005, TASK-016, TASK-017, TASK-018, TASK-019, TASK-020, TASK-021, TASK-022, DEC-014, DEC-015, DEC-016, DEC-024

## CONTEXTO

A especificação exige BCrypt, JWT, HTTPS, CORS, rate limiting, headers, soft delete e validação. O assessment também encontrou vulnerabilidades de dependência.

## OBJETIVO

Verificar controles de segurança e dependências antes do release.

## O QUE VOCÊ DEVE FAZER

1. Auditar NuGet e npm.
2. Tratar ou registrar decisão para cada finding alto.
3. Testar HTTPS, CORS, headers e rate limit.
4. Testar que secrets não aparecem em source, logs ou responses.
5. Testar soft delete e exposição de dados sensíveis.
6. Revisar error handling e logging.
7. Produzir relatório de segurança.

## ONDE TRABALHAR

- Manifests e lockfiles
- Configuração de Api/Infrastructure
- Testes de segurança
- `docs/security.md`

## NÃO FAÇA

- Não aplicar `audit fix` sem revisar impacto.
- Não aceitar finding alto silenciosamente.
- Não remover protection para fazer build passar.
- Não registrar secrets em relatório.

## REGRAS TÉCNICAS

- Controles da especificação são obrigatórios.
- Findings precisam de owner, status e impacto.
- Ausência de scanner é NOT_EXECUTED.

## CRITÉRIOS DE ACEITE

- [ ] Auditorias executadas ou explicitamente bloqueadas.
- [ ] Findings altos tratados/decididos.
- [ ] Controles têm evidência.
- [ ] Nenhum secret é exposto.
- [ ] Relatório de segurança criado.

## TESTES OBRIGATÓRIOS

- CORS allow/deny.
- Headers.
- Rate limit.
- JWT/refresh/authz.
- Error response e logs.

## COMO VALIDAR

```text
dotnet list EnterpriseManagement.slnx package --vulnerable --include-transitive
npm audit --audit-level=high
dotnet test EnterpriseManagement.slnx
npm test
npm run lint
npm run build
```

## EVIDÊNCIAS

- Relatórios de auditoria.
- Testes de headers/CORS/auth.
- Requests/responses sanitizados.
- Registro de findings.

## DEFINITION OF DONE

- [ ] Auditoria concluída.
- [ ] Findings tratados.
- [ ] Segurança validada.

## PRÓXIMA TASK

TASK-056 — Atualizar README e setup.

# TASK-056 — Atualizar README e setup

STATUS: BLOCKED

EPIC: EPIC-009

FEATURE: FEAT-009.1

SIZE: M

DEPENDS ON: TASK-002, TASK-011, DEC-020, DEC-023

## CONTEXTO

O README atual lista tecnologias, mas não explica como restaurar, configurar, conectar o PostgreSQL, aplicar migrations ou executar testes. `docs/discovery.md` e `docs/planning.md` estão vazios.

## OBJETIVO

Documentar o setup reproduzível do ambiente sem incluir secrets.

## O QUE VOCÊ DEVE FAZER

1. Documentar pré-requisitos de .NET, Node e PostgreSQL.
2. Documentar restore/install.
3. Documentar variáveis de ambiente e origem dos secrets.
4. Documentar banco, migrations e seed.
5. Documentar execução do backend/frontend.
6. Documentar build, lint e testes existentes.
7. Documentar troubleshooting básico.
8. Validar os comandos em ambiente controlado.

## ONDE TRABALHAR

- `README.md`
- `docs/setup.md` ou documento equivalente
- `docs/discovery.md` e `docs/planning.md`, se o processo do projeto exigir.

## NÃO FAÇA

- Não documentar comando inexistente.
- Não includer senha, connection string real ou key.
- Não assumir Docker sem decisão.
- Não alterar a especificação.

## REGRAS TÉCNICAS

- Comandos devem refletir o repositório real.
- `npm test` só deve ser documentado após TASK-015.
- Setup de banco segue DEC-020/023.

## CRITÉRIOS DE ACEITE

- [ ] Setup permite reproduzir o projeto.
- [ ] Variáveis e secrets estão descritos por nome.
- [ ] Banco e migrations possuem comandos reais.
- [ ] Testes e build possuem comandos reais.
- [ ] Nenhum secret está no documento.

## TESTES OBRIGATÓRIOS

- Executar setup em ambiente limpo ou registrar `NOT_EXECUTED`.
- Revisar todos os comandos contra manifests reais.

## COMO VALIDAR

```text
dotnet restore EnterpriseManagement.slnx
dotnet build EnterpriseManagement.slnx
npm ci
npm run lint
npm run build
```

## EVIDÊNCIAS

- README atualizado.
- Log de setup.
- Lista de comandos verificados.

## DEFINITION OF DONE

- [ ] Setup reproduzível.
- [ ] Comandos reais documentados.
- [ ] Segredos protegidos.

## PRÓXIMA TASK

TASK-057 — Documentar API, arquitetura e decisões.

# TASK-057 — Documentar API, arquitetura e decisões

STATUS: BLOCKED

EPIC: EPIC-009

FEATURE: FEAT-009.1

SIZE: M

DEPENDS ON: TASK-001, TASK-004, TASK-022, TASK-028, TASK-033, TASK-038, TASK-045, TASK-049

## CONTEXTO

A documentação atual cobre apenas a intenção inicial do frontend. A API, o modelo relacional, RBAC, decisões e operação precisam ser documentados junto com o código.

## OBJETIVO

Manter documentação técnica coerente com a implementação e com a especificação.

## O QUE VOCÊ DEVE FAZER

1. Documentar arquitetura e responsabilidades dos cinco projetos.
2. Documentar as 24 rotas, auth, request, response e erros.
3. Documentar schema, relationships, soft delete e refresh token.
4. Documentar decisões aprovadas esuggestions.
5. Documentar segurança, CORS, rate limit, headers e secrets.
6. Gerar OpenAPI atualizado.
7. Corrigir `docs/architecture.md`, discovery e planning quando necessário.

## ONDE TRABALHAR

- `docs/architecture.md`
- `docs/api.md`
- `docs/decisions/`
- OpenAPI gerado pela API
- README

## NÃO FAÇA

- Não documentar endpoint inexistente como concluído.
- Não esconder decisão pendente.
- Não expor token ou senha em exemplo.
- Não substituir a especificação.

## REGRAS TÉCNICAS

- Documentação deve refletir o código real.
- Requisitos confirmados e decisões devem ser distinguíveis.
- APIs devem ter autorização e erros documentados.

## CRITÉRIOS DE ACEITE

- [ ] Todas as rotas têm documentação.
- [ ] Schema e relationships estão documentados.
- [ ] DECs têm status e impacto.
- [ ] OpenAPI é gerado e conferido.
- [ ] README e architecture estão coerentes.

## TESTES OBRIGATÓRIOS

- Validar OpenAPI.
- Conferir links e IDs.
- Comparar rotas documentadas com endpoints implementados.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
npm run build
```

A validação do OpenAPI deve usar o mecanismo aprovado na TASK-004/005.

## EVIDÊNCIAS

- Documentos versionados.
- OpenAPI gerado.
- Relatório de revisão.

## DEFINITION OF DONE

- [ ] API documentada.
- [ ] Arquitetura atualizada.
- [ ] Decisões rastreáveis.

## PRÓXIMA TASK

TASK-058 — Criar pipeline CI/CD.

# TASK-058 — Criar pipeline CI/CD

STATUS: BLOCKED

EPIC: EPIC-009

FEATURE: FEAT-009.2

SIZE: M

DEPENDS ON: DEC-021, TASK-014, TASK-015, TASK-052, TASK-053, TASK-054, TASK-055

## CONTEXTO

Não existe workflow de CI/CD. A plataforma não foi definida pela especificação e não deve ser assumida silenciosamente.

## OBJETIVO

Automatizar restore, build, lint e testes na plataforma aprovada, com promotion gate.

## O QUE VOCÊ DEVE FAZER

1. Registrar decisão de plataforma.
2. Fixar versões de runtime e Node.
3. Configurar restore de NuGet e `npm ci`.
4. Executar backend build e tests.
5. Executar frontend lint, tests e build.
6. Executar E2E quando o ambiente estiver disponível.
7. Configurar artifacts, logs e secrets de CI.
8. Bloquear promoção quando uma etapa obrigatória falhar.

## ONDE TRABALHAR

- Plataforma escolhida após DEC-021
- Scripts de CI
- Documentação de secrets

## NÃO FAÇA

- Não assumir GitHub Actions, Azure ou GitLab sem decisão.
- Não publicar produção.
- Não exibir secrets nos logs.
- Não remover etapa que falha.

## REGRAS TÉCNICAS

- Build, testes, lint e frontend build são gates.
- A plataforma é DEC-021.
- Versões são DEC-022.

## CRITÉRIOS DE ACEITE

- [ ] Pipeline executa todos os comandos reais.
- [ ] Falha de qualquer gate bloqueia promoção.
- [ ] Secrets usam mechanism da plataforma.
- [ ] Artefatos e logs são preservados.
- [ ] Plataforma e promotion estão documentados.

## TESTES OBRIGATÓRIOS

- Pipeline em branch de validação.
- Falha proposital de um comando, se seguro.
- Verificação de secrets nos logs.

## COMO VALIDAR

Os comandos executados pelo pipeline devem ser os mesmos documentados no repositório:

```text
dotnet restore EnterpriseManagement.slnx
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
npm ci
npm run lint
npm test
npm run build
npx playwright test
```

## EVIDÊNCIAS

- URL/log do pipeline.
- Matriz de jobs.
- Resultado de cada gate.

## DEFINITION OF DONE

- [ ] CI configurado.
- [ ] Gates executados.
- [ ] Promotion bloqueada em falha.

## PRÓXIMA TASK

TASK-059 — Executar release checklist e smoke tests.

# TASK-059 — Executar release checklist e smoke tests

STATUS: BLOCKED

EPIC: EPIC-009

FEATURE: FEAT-009.3

SIZE: M

DEPENDS ON: TASK-052, TASK-053, TASK-054, TASK-055, TASK-056, TASK-057, TASK-058, DEC-017, DEC-021, DEC-023

## CONTEXTO

O critério global exige build, testes, lint, documentação, evidências, APIs documentadas, segurança validada e release publicada. O destino de release ainda é DEC-021.

## OBJETIVO

Executar o checklist de release e smoke tests sem declarar sucesso por build isolado.

## O QUE VOCÊ DEVE FAZER

1. Confirmar build de backend e frontend.
2. Confirmar testes, lint e E2E ou registrar `NOT_EXECUTED`.
3. Confirmar findings de segurança.
4. Confirmar migrations em ambiente aprovado.
5. Executar smoke de Login, Users, Clients, Products, Orders e Dashboard.
6. Confirmar docs e OpenAPI.
7. Registrar rollback e recuperação.
8. Emitir decisão go/no-go.

## ONDE TRABALHAR

- Checklist de release
- Ambiente de staging/aprovado
- Playwright/smoke tests
- Documentação de release

## NÃO FAÇA

- Não publicar em provider não aprovado.
- Não apagar banco.
- Não ignorar finding alto.
- Não declarar PASS sem evidência.

## REGRAS TÉCNICAS

- Os seis fluxos da especificação precisam ser verificados.
- Migrations e ambiente seguem DEC-023.
- Release target segue DEC-021.

## CRITÉRIOS DE ACEITE

- [ ] Cada gate tem resultado e evidência.
- [ ] Seis fluxos são verificados.
- [ ] Findings têm decisão.
- [ ] Rollback está documentado.
- [ ] Go/no-go é explícito.

## TESTES OBRIGATÓRIOS

- Smoke tests dos seis fluxos.
- Build/test/lint/security gates.
- Verificação de migrations.

## COMO VALIDAR

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
npm run lint
npm test
npm run build
npx playwright test
```

## EVIDÊNCIAS

- Checklist assinado.
- Logs de comandos.
- Reports E2E.
- Findings e decisões.
- Resultado go/no-go.

## DEFINITION OF DONE

- [ ] Release checklist executado.
- [ ] Evidências anexadas.
- [ ] Decisão de release registrada.

## PRÓXIMA TASK

TASK-060 — Fechar rastreabilidade e readiness.

# TASK-060 — Fechar rastreabilidade e readiness

STATUS: BLOCKED

EPIC: EPIC-009

FEATURE: FEAT-009.3

SIZE: S

DEPENDS ON: TASK-059

## CONTEXTO

O projeto precisa manter a cadeia Specification → Epic → Feature → Task → Code → Test → Verification. Findings e decisões não podem desaparecer no fechamento.

## OBJETIVO

Auditar a rastreabilidade e emitir o estado final do projeto sem transformar ausência de evidência em PASS.

## O QUE VOCÊ DEVE FAZER

1. Conferir se cada requisito da especificação possui task e teste.
2. Conferir o status das tasks e dependências.
3. Listar criteria NOT_VERIFIED, FAIL e NEEDS_REVIEW.
4. Conferir decisões pendentes e findings abertos.
5. Conferir links de verification reports.
6. Emitir readiness final e próxima ação.

## ONDE TRABALHAR

- `docs/DEVELOPMENT_BACKLOG.md`
- `docs/verification/`
- Relatórios de testes e release

## NÃO FAÇA

- Não implementar correções.
- Não marcar PASS por ausência de evidência.
- Não apagar findings.
- Não transformar melhoria opcional em bloqueio.

## REGRAS TÉCNICAS

- PASS exige todos os critérios obrigatórios comprovados.
- NEEDS_REVIEW significa evidência insuficiente.
- FAIL significa critério obrigatório quebrado.
- Code quality é separado de aceite.

## CRITÉRIOS DE ACEITE

- [ ] Nenhum requisito explícito fica sem rastreabilidade.
- [ ] Tasks concluídas têm verificação.
- [ ] Falhas e decisões abertas estão visíveis.
- [ ] Relatório final tem próxima ação.

## TESTES OBRIGATÓRIOS

- Revisão de matriz e relatórios.
- Não criar novos testes de runtime nesta task.

## COMO VALIDAR

- Usar `git status`, leitura de docs e relatórios existentes.
- Não executar comandos destrutivos.

## EVIDÊNCIAS

- Matriz final.
- Relatório de status.
- Lista de findings/decisions.

## DEFINITION OF DONE

- [ ] Rastreabilidade conferida.
- [ ] Status final honesto.
- [ ] Próxima ação identificada.

## PRÓXIMA TASK

Nenhuma. Aguardar decisão do responsável pelo release.

## Decisions Made

As decisões abaixo foram aprovadas para este projeto de portfolio e estão registradas em `docs/decisions/`. Elas complementam a especificação; não alteram os requisitos explícitos. O índice completo e as racionalizações estão em [`docs/decisions/README.md`](decisions/README.md).

| ID      | Decision                                                       | Impact                             | Status   |
| ------- | -------------------------------------------------------------- | ---------------------------------- | -------- |
| DEC-001 | Formato de erro, status codes e payload de validação           | Todos os endpoints, frontend e E2E | APPROVED |
| DEC-002 | Query e response de paginação/filtros                          | Todas as listas e telas            | APPROVED |
| DEC-003 | DTOs, required, lengths, email, datas e response fields        | Contratos de API                   | APPROVED |
| DEC-004 | Entidades que possuem soft delete                              | EF queries, migrations e DELETE    | APPROVED |
| DEC-005 | Delete behavior de Client/Product com Order/OrderItem          | Integridade referencial            | APPROVED |
| DEC-006 | PF/PJ, documento, normalização e unicidade                     | Client DTOs, API e UI              | APPROVED |
| DEC-007 | Tipo, escala, moeda e rounding                                 | Product e Order totals             | APPROVED |
| DEC-008 | Transições e reversibilidade dos status                        | Order state machine                | APPROVED |
| DEC-009 | Quantidade, snapshot, edição, atomicidade e concorrência       | OrderItem e criação de Order       | APPROVED |
| DEC-010 | Password policy, email unique, roles, bootstrap e último ADMIN | User e login                       | APPROVED |
| DEC-011 | Algoritmo, issuer, audience e signing key                      | JWT                                | APPROVED |
| DEC-012 | Rotação, reuso, logout e efeito de senha/role/delete           | Refresh token                      | APPROVED |
| DEC-013 | Storage e uso de tokens no frontend                            | Segurança da SPA                   | APPROVED |
| DEC-014 | Origens CORS por ambiente                                      | Integração browser/API             | APPROVED |
| DEC-015 | Limite, janela e resposta de rate limiting                     | Segurança e abuso                  | APPROVED |
| DEC-016 | Headers e postura HTTPS/HSTS                                   | Transporte e headers               | APPROVED |
| DEC-017 | Cards, período, métricas e estados do Dashboard                | Dashboard                          | APPROVED |
| DEC-018 | Paths, layout, loading, empty, error e navegação               | Frontend                           | APPROVED |
| DEC-019 | Controllers, minimal APIs ou outro estilo                      | API e composition root             | APPROVED |
| DEC-020 | Banco de teste e métrica de cobertura                          | Testes de integração/E2E           | APPROVED |
| DEC-021 | Plataforma de CI/CD e destino de release                       | Automation e promoção              | APPROVED |
| DEC-022 | Versões de .NET, Node, React e pacotes                         | Build e CI                         | APPROVED |
| DEC-023 | Connection string, migrations, seed e promoção de schema       | Banco e ambiente                   | APPROVED |
| DEC-024 | Logs, correlation ID e evidência/auditoria                     | Observabilidade e QA               | APPROVED |

### Decisões já confirmadas pela especificação

- Roles: ADMIN e OPERATOR.
- Clientes: PF e PJ.
- Produtos sem estoque no MVP.
- Status: Pendente, Concluído e Cancelado.
- Dashboard apenas com indicadores.
- Soft delete aprovado, com escopo a detalhar.
- JWT com 15 minutos e claims `sub`, `email`, `role`.
- Refresh token com sete dias, persistido e revogável.

## API Contract Summary

O formato final dos campos e erros depende de DEC-001 a DEC-003. A tabela preserva as rotas e a matriz de autorização que são requisitos da especificação.

| Método e rota                   | Autorização                   | Regra principal                 |
| ------------------------------- | ----------------------------- | ------------------------------- |
| `POST /api/auth/login`          | Pública                       | Credenciais e emissão de sessão |
| `POST /api/auth/refresh`        | Token/sessão conforme DEC-012 | Renovação e revogação           |
| `POST /api/auth/logout`         | Token/sessão conforme DEC-012 | Revogação                       |
| `GET /api/users`                | ADMIN                         | Lista paginada                  |
| `GET /api/users/{id}`           | ADMIN                         | Detalhe                         |
| `POST /api/users`               | ADMIN                         | Create e hash BCrypt            |
| `PUT /api/users/{id}`           | ADMIN                         | Update                          |
| `DELETE /api/users/{id}`        | ADMIN                         | Soft delete aprovado            |
| `GET /api/clients`              | ADMIN/OPERATOR                | Lista paginada                  |
| `GET /api/clients/{id}`         | ADMIN/OPERATOR                | Detalhe                         |
| `POST /api/clients`             | ADMIN/OPERATOR                | Create PF/PJ                    |
| `PUT /api/clients/{id}`         | ADMIN/OPERATOR                | Update                          |
| `DELETE /api/clients/{id}`      | ADMIN/OPERATOR                | Soft delete aprovado            |
| `GET /api/products`             | ADMIN/OPERATOR                | Lista paginada                  |
| `GET /api/products/{id}`        | ADMIN/OPERATOR                | Detalhe                         |
| `POST /api/products`            | ADMIN/OPERATOR                | Create sem estoque              |
| `PUT /api/products/{id}`        | ADMIN/OPERATOR                | Update                          |
| `DELETE /api/products/{id}`     | ADMIN/OPERATOR                | Soft delete aprovado            |
| `GET /api/orders`               | ADMIN/OPERATOR                | Lista paginada                  |
| `GET /api/orders/{id}`          | ADMIN/OPERATOR                | Detalhe com itens               |
| `POST /api/orders`              | ADMIN/OPERATOR                | Create e total                  |
| `PUT /api/orders/{id}`          | ADMIN/OPERATOR                | Update conforme status          |
| `PATCH /api/orders/{id}/status` | ADMIN/OPERATOR                | Transição aprovada              |
| `GET /api/dashboard`            | ADMIN/OPERATOR                | Cards/indicadores               |

Não há rota DELETE de Order na especificação e nenhuma deve ser inventada.

## Test and Validation Strategy

### Backend

- xUnit para domínio e aplicação.
- Testes de integração para EF Core, PostgreSQL, endpoints, auth e RBAC.
- Banco de teste isolado conforme DEC-020.
- Casos obrigatórios: happy path, input inválido, unauthorized, forbidden, not found, duplicate e estado inválido.

### Frontend

- Vitest e React Testing Library.
- Testes de services, hooks, forms, routes, loading, empty, error e success.
- `npm test` somente após TASK-015.

### E2E

- Playwright.
- Fluxos: Login, Users, Clients, Products, Orders e Dashboard.
- Dados e ambiente isolados.
- `npx playwright test` somente após TASK-015.

### Comandos atuais

```text
dotnet build EnterpriseManagement.slnx
dotnet test EnterpriseManagement.slnx
npm run lint
npm run build
```

`npm test` e `npx playwright test` não são scripts atuais; devem ser adicionados pela TASK-015.

## Dependency Graph and Critical Path

```text
TASK-001 Decisions
        ↓
TASK-002/003 Configuration and EF
        ↓
TASK-004 API contracts
        ↓
TASK-006..010 Domain and persistence
        ↓
TASK-011 Migration
        ↓
TASK-016..024 Authentication and security
        ↓
TASK-026..040 Users, Clients, Products
        ↓
TASK-041..047 Orders
        ↓
TASK-048..051 Dashboard
        ↓
TASK-052..055 Quality and security
        ↓
TASK-056..060 Documentation, CI/CD and release
```

Clients e Products podem ser trabalhados em paralelo depois da Foundation e das decisões de segurança. Orders depende dos contratos e relacionamentos de ambos.

## Execution Roadmap

| Fase              | Tasks               | Saída                                           |
| ----------------- | ------------------- | ----------------------------------------------- |
| 0. Decisões       | TASK-001            | Decisões aprovadas ou bloqueadas explicitamente |
| 1. Foundation     | TASK-002 a TASK-015 | Configuração, banco, shell e testes             |
| 2. Auth           | TASK-016 a TASK-025 | Login, JWT, refresh/logout, RBAC e UI auth      |
| 3. Core resources | TASK-026 a TASK-040 | Users, Clients e Products                       |
| 4. Orders         | TASK-041 a TASK-047 | Pedidos consistentes e testados                 |
| 5. Dashboard      | TASK-048 a TASK-051 | Cards e indicadores                             |
| 6. Quality        | TASK-052 a TASK-055 | Testes, E2E e hardening                         |
| 7. Release        | TASK-056 a TASK-060 | Docs, CI/CD, smoke e readiness                  |

## Vertical Slices

1. **Foundation:** configuração, schema e shell executável.
2. **Authentication:** login, refresh, logout e RBAC.
3. **Users:** ADMIN gerencia usuários.
4. **Clients:** ADMIN/OPERATOR gerenciam PF/PJ.
5. **Products:** ADMIN/OPERATOR mantêm catálogo sem estoque.
6. **Orders:** criação, itens, total e status.
7. **Dashboard:** indicadores autorizados.
8. **Hardening/Release:** auditoria, E2E, docs e pipeline.

Cada slice deve terminar com build/teste/demo ou com `NOT_EXECUTED` explicitamente justificado.

## Traceability Matrix

| Requirement                                      | Epic         | Feature          | Tasks                                                                              | Planned tests                                  |
| ------------------------------------------------ | ------------ | ---------------- | ---------------------------------------------------------------------------------- | ---------------------------------------------- |
| React + TypeScript + .NET + EF Core + PostgreSQL | EPIC-001     | FEAT-001.1–001.6 | TASK-002, TASK-003, TASK-006–015                                                   | TEST-INFRA-001                                 |
| Login                                            | EPIC-002     | FEAT-002.1       | TASK-016, TASK-018, TASK-024                                                       | TEST-AUTH-001, TEST-E2E-001                    |
| Users                                            | EPIC-003     | FEAT-003.1–003.3 | TASK-026–030                                                                       | TEST-USERS-001, TEST-E2E-002                   |
| Clients PF/PJ                                    | EPIC-004     | FEAT-004.1–004.3 | TASK-031–035                                                                       | TEST-CLIENTS-001, TEST-E2E-003                 |
| Products sem estoque                             | EPIC-005     | FEAT-005.1–005.3 | TASK-036–040                                                                       | TEST-PRODUCTS-001, TEST-E2E-004                |
| Orders/status                                    | EPIC-006     | FEAT-006.1–006.4 | TASK-041–047                                                                       | TEST-ORDERS-001, TEST-ORDERS-002, TEST-E2E-005 |
| Dashboard                                        | EPIC-007     | FEAT-007.1–007.3 | TASK-048–051                                                                       | TEST-DASH-001, TEST-E2E-006                    |
| JWT                                              | EPIC-002     | FEAT-002.2       | TASK-017, TASK-022, TASK-025                                                       | TEST-AUTH-002                                  |
| Refresh/logout                                   | EPIC-002     | FEAT-002.3       | TASK-019–021, TASK-025                                                             | TEST-AUTH-003, TEST-AUTH-004                   |
| RBAC                                             | EPIC-002     | FEAT-002.4       | TASK-022, TASK-025, endpoint tasks                                                 | TEST-RBAC-001                                  |
| Paginação/filtros                                | EPIC-001–007 | FEAT-001.4       | TASK-004 e consultas                                                               | TEST-PAGING-001                                |
| Soft delete                                      | EPIC-001     | FEAT-001.3       | TASK-006, TASK-010, TASK-011                                                       | TEST-DB-001                                    |
| BCrypt                                           | EPIC-002     | FEAT-002.1       | TASK-016, TASK-018, TASK-025                                                       | TEST-AUTH-001                                  |
| HTTPS/CORS/rate limit/headers                    | EPIC-001/002 | FEAT-001.4/002.6 | TASK-005, TASK-055                                                                 | TEST-SEC-001                                   |
| Testes                                           | EPIC-008     | FEAT-008.1–008.4 | TASK-014, TASK-015, TASK-025, TASK-030, TASK-035, TASK-040, TASK-047, TASK-051–055 | Todos os testes planejados                     |
| Docs/API                                         | EPIC-009     | FEAT-009.1       | TASK-056, TASK-057                                                                 | TEST-DOC-001                                   |
| CI/CD                                            | EPIC-009     | FEAT-009.2       | TASK-058                                                                           | TEST-CI-001                                    |
| Release                                          | EPIC-009     | FEAT-009.3       | TASK-059, TASK-060                                                                 | Release checklist                              |

## Risk Register

| ID    | Risk                                              | Impact | Probability | Mitigation                          | Trigger                            |
| ----- | ------------------------------------------------- | ------ | ----------- | ----------------------------------- | ---------------------------------- |
| R-001 | Contratos ambíguos geram incompatibilidade API/UI | HIGH   | HIGH        | TASK-001, DEC-001–003               | DTO implementado sem aprovação     |
| R-002 | Soft delete/delete behavior corrompe Orders       | HIGH   | MEDIUM      | DEC-004/005 e testes de relação     | DELETE com referência              |
| R-003 | Refresh token reutilizado ou storage inseguro     | HIGH   | MEDIUM      | DEC-012/013 e testes de revogação   | Refresh após logout                |
| R-004 | Autorização apenas no frontend                    | HIGH   | MEDIUM      | TASK-022 e TEST-RBAC-001            | Endpoint retorna 200 indevidamente |
| R-005 | Dependências vulneráveis chegam ao release        | HIGH   | HIGH        | TASK-055                            | Scanner retorna high               |
| R-006 | Banco de teste/migration indisponível             | HIGH   | HIGH        | TASK-011, DEC-020/023               | Testes de integração não executam  |
| R-007 | Total/status de Order inconsistente               | HIGH   | MEDIUM      | DEC-007–009, TASK-043/044           | Total diverge do banco             |
| R-008 | Único desenvolvedor perde contexto                | MEDIUM | MEDIUM      | Backlog, dependências e verificação | Task sem evidência                 |
| R-009 | CI inexistente permite regressão                  | MEDIUM | HIGH        | TASK-058                            | Mudança sem pipeline               |
| R-010 | Versões locais não funcionam no CI                | MEDIUM | MEDIUM      | DEC-022                             | Build local passa e CI falha       |

## Definition of Done

Uma task só pode estar `DONE` quando:

- [ ] Todos os critérios obrigatórios estiverem comprovados.
- [ ] Testes relevantes tiverem sido executados.
- [ ] Build/lint aplicáveis tiverem resultado.
- [ ] Evidência for reproduzível.
- [ ] Dependências e documentação estiverem atualizadas.
- [ ] Secrets não estiverem expostos.
- [ ] Uma verificação `/verify TASK-ID` tiver sido concluída.
- [ ] O relatório não tiver FAIL ou NEEDS_REVIEW sem próxima ação.

## Final Project Checklist

### Foundation

- [ ] Decisões registradas.
- [ ] Packages e ambiente configurados.
- [ ] EF Core/PostgreSQL e migration funcionando.
- [ ] API pipeline seguro.
- [ ] SPA shell e API client functioning.

### Product

- [ ] Auth e RBAC funcionando.
- [ ] Users, Clients, Products, Orders e Dashboard implementados.
- [ ] Paginação, filtros, soft delete e erros conforme decisões.

### Quality

- [ ] xUnit, integração, Vitest/RTL e Playwright configurados.
- [ ] Casos positivos e negativos testados.
- [ ] Segurança e dependências auditadas.

### Release

- [ ] README, setup, API, architecture e decisões documentados.
- [ ] CI/CD executa gates reais.
- [ ] Migrations, smoke tests, rollback e evidências concluídos.
- [ ] Release readiness decidido explicitamente.

**Estado após a execução de TASK-001:** TASK-001 está DONE. TASK-002, TASK-004, TASK-006, TASK-007, TASK-008, TASK-009, TASK-012, TASK-014, TASK-015 e TASK-048 estão READY; as demais permanecem BLOCKED por dependências de implementação. Nenhuma funcionalidade de produto foi implementada.

<!-- END -->
