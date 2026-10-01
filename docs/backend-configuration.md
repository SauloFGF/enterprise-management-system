# Backend Configuration

## Overview

This document describes how to configure the backend environment for the Enterprise Management System.

## Prerequisites

- .NET SDK `10.0.41` (fixed via `global.json`)
- PostgreSQL 16+
- Node.js `24.21.0` (for frontend)

## Restore and Build

```bash
# Restore NuGet packages
dotnet restore EnterpriseManagement.slnx

# Build the solution
dotnet build EnterpriseManagement.slnx

# Run vulnerability audit
dotnet list EnterpriseManagement.slnx package --vulnerable --include-transitive
```

## Environment Configuration

### Configuration Sources

The backend reads configuration from (in order of precedence):

1. Environment variables (e.g., `ConnectionStrings__Postgres`, `Jwt__Key`)
2. User secrets (Development only)
3. `appsettings.{Environment}.json`
4. `appsettings.json`

### Database (PostgreSQL)

DEC-023 specifies that the connection string is read from `ConnectionStrings__Postgres` or user secrets.

**Development (User Secrets):**

```bash
cd src/EnterpriseManagement.Api
dotnet user-secrets set "ConnectionStrings:Postgres" "Host=localhost;Port=5432;Database=enterprise_management;Username=postgres;Password=your_password"
```

**Environment Variable:**

```bash
# Windows (PowerShell)
$env:ConnectionStrings__Postgres="Host=localhost;Port=5432;Database=enterprise_management;Username=postgres;Password=your_password"

# Linux/macOS
export ConnectionStrings__Postgres="Host=localhost;Port=5432;Database=enterprise_management;Username=postgres;Password=your_password"
```

### JWT Configuration

DEC-011 specifies HS256 with issuer `enterprise-management-api`, audience `enterprise-management-web`, and a minimum 32-byte signing key.

**Development (User Secrets):**

```bash
dotnet user-secrets set "Jwt:Key" "your-32-byte-minimum-signing-key-here"
```

**Environment Variable:**

```bash
$env:Jwt__Key="your-32-byte-minimum-signing-key-here"
```

### CORS

DEC-014 specifies that development allows only `http://localhost:5173`. Production reads `FRONTEND_ORIGIN`.

**Production Environment Variable:**

```bash
$env:FRONTEND_ORIGIN="https://your-production-domain.com"
```

### Admin Seed (Development/Test only)

DEC-023 specifies that the seed reads `ADMIN_SEED_EMAIL` and `ADMIN_SEED_PASSWORD` only in Development/Test.

```bash
$env:ADMIN_SEED_EMAIL="admin@example.com"
$env:ADMIN_SEED_PASSWORD="ChangeMe123!"
```

> **Note:** The current `UserSeed` implementation reads `SEED_ADMIN_PASSWORD` and `SEED_OPERATOR_PASSWORD`. These environment variable names should be aligned with DEC-023 in a future task.

## Environment File Example

See `backend/.env.example` for a template of all supported environment variables.

> **IMPORTANT:** Never commit the `.env` file with real secrets. The `.env.example` file contains only placeholder values.

## Security Headers

DEC-016 specifies the following headers are returned by the API:

| Header | Value |
|--------|-------|
| `X-Content-Type-Options` | `nosniff` |
| `X-Frame-Options` | `DENY` |
| `Referrer-Policy` | `no-referrer` |
| `Permissions-Policy` | `camera=(), microphone=(), geolocation=()` |
| `Content-Security-Policy` | Restrictive (configured in `SecurityHeadersOptions`) |

## Rate Limiting

DEC-015 specifies:

| Endpoint | Limit |
|----------|-------|
| Login | 10 requests/minute per IP |
| Refresh | 10 requests/minute per IP |
| Authenticated API | 120 requests/minute per user (fallback to IP) |

## Migrations

Migrations are applied explicitly (not on startup):

```bash
# Create a new migration
dotnet ef migrations add MigrationName --project src/EnterpriseManagement.Infrastructure --startup-project src/EnterpriseManagement.Api

# Apply migrations
dotnet ef database update --project src/EnterpriseManagement.Infrastructure --startup-project src/EnterpriseManagement.Api

# Rollback (last migration)
dotnet ef database update PreviousMigrationName --project src/EnterpriseManagement.Infrastructure --startup-project src/EnterpriseManagement.Api
```

## Dependency Audit

See `docs/dependency-findings.md` for the current vulnerability audit results and decisions.
