# .NET Version Upgrade - DynSec Solution

## Preferences
- **Flow Mode**: Guiado (pausa após cada stage para revisão)
- **Target Framework**: .NET 10.0 (LTS)
- **Language Version**: Auto-upgrade para C# 13
- **Package Management**: Individual (approach atual)

## Source Control
- **Source Branch**: dev
- **Working Branch**: net10-upgrade (existente)
- **Commit Strategy**: A cada tarefa completada

## Strategy
**Selected**: Bottom-Up
**Rationale**: Solução com 5 níveis de dependência bem definidos. Bottom-Up permite validar bibliotecas base primeiro (Model, MQTT, Web, ServiceDefaults) antes de mover para camadas dependentes (Protocol → GraphQL → API → Aspire).

### Execution Constraints
- **Strict Tier Ordering**: Foundation (01) → Protocol (02) → GraphQL (03) → API (04) → Aspire (05) → Final Validation (06)
- **Validation After Each Tier**: Cada tier deve fazer build e passar em testes antes de passar para o próximo
- **Commit After Each Task**: Checkpoint claro após cada tier completada
- **Segurança**: OpenTelemetry vulnerability (1.12.0 → 1.15.3) incluída na tier 01

## Key Decisions Log
- Usuário escolheu modo **Guiado** para revisão detalhada
- Alterações pendentes foram descartadas antes de inicializar
- Estratégia Bottom-Up selecionada baseada na profundidade de dependências (5 níveis)
- Upgrade de OpenTelemetry (vulnerabilidade crítica) incluído na foundation tier

