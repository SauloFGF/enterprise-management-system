# Dependency Findings — TASK-002

## Data: 01/10/2026

## Resumo

| Pacote | Versão | Severidade | Status | Ação |
|--------|--------|------------|--------|------|
| Microsoft.OpenApi | 2.0.0 | Alta (GHSA-v5pm-xwqc-g5wc) | PENDENTE | Avaliar upgrade quando Microsoft.AspNetCore.OpenApi publicar versão corrigida |

## Detalhamento

### 1. Microsoft.OpenApi 2.0.0

- **Vulnerabilidade:** GHSA-v5pm-xwqc-g5wc
- **Impacto:** Alta severidade
- **Origem:** Transitivo (dependência do Microsoft.AspNetCore.OpenApi 10.0.8)
- **Projetos afetados:** EnterpriseManagement.Api, EnterpriseManagement.Tests
- **Decisão necessária:** Aguardar atualização do Microsoft.AspNetCore.OpenApi ou substituir por versão corrigida
- **Task de acompanhamento:** Criar task para monitorar e aplicar upgrade quando disponível

## Comando de Auditoria

```bash
dotnet list EnterpriseManagement.slnx package --vulnerable --include-transitive
```

## Histórico

| Data | Finding | Ação |
|------|---------|------|
| 30/09/2026 | Microsoft.OpenApi 2.0.0 (Alta) | Registrado |
| 30/09/2026 | Conflito EF Core 10.0.4 vs 10.0.12 | Resolvido — PackageReference explícito 10.0.12 adicionado |
| 01/10/2026 | Conflito EF Core 10.0.4 vs 10.0.12 (Tests) | Resolvido — PackageReference explícito 10.0.12 adicionado ao projeto Tests |
