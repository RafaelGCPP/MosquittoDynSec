# 05-aspire-application: Upgrade Aspire orchestration app

DynSec.Aspire (21 LOC) depends on DynSec.Web and DynSec.API (both now net10.0). Orchestrates the entire application. Final step in upgrade.

**Included work**:
- Update target framework to net10.0
- Update NuGet packages: Aspire.Hosting.* packages
- Validate DynSec.Aspire builds successfully against all upgraded dependencies
- Full end-to-end validation: Aspire app can start and orchestrate all services

**Done when**: DynSec.Aspire targets net10.0, builds without errors or warnings, solution builds successfully, Aspire app validates all services.
