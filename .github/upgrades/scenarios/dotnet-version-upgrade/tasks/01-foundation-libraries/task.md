# 01-foundation-libraries: Upgrade core foundation projects (Model, MQTT, Web, ServiceDefaults)

Foundation projects have no inter-project dependencies and form the base layer. Upgrade these four projects together: DynSec.Model (1,751 LOC, core models), DynSec.MQTT (76 LOC, MQTT client wrapper), DynSec.Web (net6.0 → net10.0, Angular SPA), and DynSec.Aspire.ServiceDefaults (127 LOC, service configuration).

**Included work**:
- Update target frameworks to net10.0 in all four project files
- Update NuGet packages: Aspire.Hosting.AppHost (9.3.0 → 13.4.0), Aspire.Hosting.NodeJs, OpenTelemetry.* packages, Microsoft.Extensions.* packages
- Address security vulnerability: OpenTelemetry.Exporter.OpenTelemetryProtocol 1.12.0 → 1.15.3
- Fix any API incompatibilities (minimal expected in these projects)
- Validate all four projects build successfully
- Run unit tests if present

**Done when**: All four foundation projects target net10.0, build without errors or warnings, and tests pass.

---

## Research Findings

### Project Assessment Summary

#### DynSec.Model
- **Current TFM**: net9.0 → **Target**: net10.0
- **SDK-style**: ✅ Yes (modern format)
- **Issues**: 1 (Mandatory: change TFM)
- **Packages**: None to update (no NuGet dependencies found in assessment)
- **Code Impact**: Minimal — no API incompatibilities expected

#### DynSec.MQTT
- **Current TFM**: net9.0 → **Target**: net10.0
- **SDK-style**: ✅ Yes
- **Issues**: 1 (Mandatory: change TFM)
- **Packages**: `MQTTnet.AspNetCore` 5.0.1.1416 (✅ already compatible, no update needed)
- **Code Impact**: Minimal — no API incompatibilities expected

#### DynSec.Web
- **Current TFM**: net6.0 → **Target**: net10.0 (big jump!)
- **SDK-style**: ✅ Yes (.esproj — Angular SPA)
- **Issues**: 1 (Mandatory: change TFM)
- **Packages**: None to update
- **Code Impact**: Minimal — Angular SPA, no .NET code changes expected

#### DynSec.Aspire.ServiceDefaults
- **Current TFM**: net9.0 → **Target**: net10.0
- **SDK-style**: ✅ Yes
- **Issues**: 6 (1 Mandatory: TFM change, 4 Potential: package upgrades, 1 Optional: security vuln)
- **Packages to Update**:
  - `Microsoft.Extensions.Http.Resilience`: 9.5.0 → **10.6.0**
  - `Microsoft.Extensions.ServiceDiscovery`: 9.3.0 → **10.6.0**
  - `OpenTelemetry.Exporter.OpenTelemetryProtocol`: 1.12.0 → **1.15.3** (🔴 **SECURITY FIX**)
  - `OpenTelemetry.Instrumentation.AspNetCore`: 1.12.0 → **1.15.2**
  - `OpenTelemetry.Instrumentation.Http`: 1.12.0 → **1.15.1**
  - `OpenTelemetry.Extensions.Hosting`: 1.12.0 (✅ compatible, optional)
  - `OpenTelemetry.Instrumentation.Runtime`: 1.12.0 (✅ compatible, optional)
- **Code Impact**: Minimal — service configuration library, no breaking changes expected

### Execution Plan

This task is **atomic** — all four projects can be upgraded independently in a single pass since they have no inter-project dependencies.

**Order**: Model → MQTT → Web → ServiceDefaults

1. Update TFM in all four `.csproj`/`.esproj` files (net9.0 → net10.0, or net6.0 → net10.0 for Web)
2. Update NuGet packages in ServiceDefaults (the only project with package updates)
3. Build all four projects
4. Verify no warnings or errors
5. Run tests (if any exist in these foundation projects)
