# 02-protocol-layer: Upgrade Protocol library

DynSec.Protocol (790 LOC) depends only on DynSec.Model (now net10.0). Contains MQTT protocol abstractions and 2 identified API issues with TimeSpan.FromSeconds. Upgrade packages and fix source incompatibilities.

**Included work**:
- Update target framework to net10.0
- Update NuGet packages: Microsoft.Extensions.* packages
- Fix 3 instances of TimeSpan.FromSeconds incompatibility (API migration)
- Validate DynSec.Protocol builds successfully and depends only on updated Model
- Run unit tests

**Done when**: DynSec.Protocol targets net10.0, builds without errors or warnings, source incompatibilities resolved, tests pass.

---

## Research Findings

### Project Assessment

- **Current TFM**: net9.0 → **Target**: net10.0
- **SDK-style**: ✅ Yes
- **Issues**: 6 (1 Mandatory: TFM change, 2 Potential: package upgrades, 3 Potential: API changes)
- **Dependencies**: DynSec.Model (now net10.0 ✅)
- **Affected Files**: 1 file with issues (DynamicSecurityRpc.cs)
- **LOC**: 790 total, ~3 LOC to fix

### Packages to Update
- `Microsoft.Extensions.DependencyInjection.Abstractions`: 9.0.5 → **10.0.8**
- `Microsoft.Extensions.Logging.Abstractions`: 9.0.5 → **10.0.8**
- `MQTTnet`: 5.0.1.1416 (✅ compatible, no update)
- `MQTTnet.Extensions.Rpc`: 5.0.1.1416 (✅ compatible, no update)
- `Serilog`: 4.3.0 (✅ compatible, no update)

### API Changes: TimeSpan.FromSeconds(Int64)

**Issue**: `.NET Core removed the overload `TimeSpan.FromSeconds(Int64)`. Only `TimeSpan.FromSeconds(Double)` is available.

**Occurrences in DynamicSecurityRpc.cs**:
1. Line 45: `disconnectTimer = new Timer(TimeSpan.FromSeconds(60))`
2. Line 85: `TimeSpan _timeout = TimeSpan.FromSeconds(10)`

**Fix**: Convert `Int64` literals to `Double` — C# will implicitly convert them:
- `TimeSpan.FromSeconds(60)` → `TimeSpan.FromSeconds(60.0)` (or just `60d`)
- `TimeSpan.FromSeconds(10)` → `TimeSpan.FromSeconds(10.0)` (or just `10d`)

### Execution Plan

1. Update TFM: net9.0 → net10.0 in DynSec.Protocol.csproj
2. Update package versions (2 packages)
3. Fix 3 occurrences of `TimeSpan.FromSeconds` API in DynamicSecurityRpc.cs
4. Build and validate (DynSec.Protocol should compile)
5. Run tests (if any)
