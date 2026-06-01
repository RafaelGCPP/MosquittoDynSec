# 02-protocol-layer: Upgrade Protocol library

DynSec.Protocol (790 LOC) depends only on DynSec.Model (now net10.0). Contains MQTT protocol abstractions and 2 identified API issues with TimeSpan.FromSeconds. Upgrade packages and fix source incompatibilities.

**Included work**:
- Update target framework to net10.0
- Update NuGet packages: Microsoft.Extensions.* packages
- Fix 3 instances of TimeSpan.FromSeconds incompatibility (API migration)
- Validate DynSec.Protocol builds successfully and depends only on updated Model
- Run unit tests

**Done when**: DynSec.Protocol targets net10.0, builds without errors or warnings, source incompatibilities resolved, tests pass.
