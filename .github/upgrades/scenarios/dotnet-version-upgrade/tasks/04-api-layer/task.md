# 04-api-layer: Upgrade API application

DynSec.API (690 LOC) depends on Model, GraphQL, Protocol, MQTT, and ServiceDefaults (all now net10.0). This is the most complex project: 19 issues identified, primarily OpenIdConnect authentication APIs (9 property changes) and 1 binary incompatibility with System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.

**Included work**:
- Update target framework to net10.0
- Update NuGet packages: Microsoft.AspNetCore.* packages (9.0.5 → 10.0.8), Scalar.AspNetCore, Serilog.AspNetCore
- Fix authentication APIs: OpenIdConnectOptions properties (Authority, ClientId, ClientSecret, MapInboundClaims, UsePkce, ResponseType, ResponseMode, GetClaimsFromUserInfoEndpoint, SignOutScheme, TokenValidationParameters)
- Fix binary incompatibility: replace System.IdentityModel.Tokens.Jwt with Microsoft.IdentityModel.Tokens
- Validate DynSec.API builds successfully against all upgraded dependencies
- Run integration tests if present

**Done when**: DynSec.API targets net10.0, builds without errors or warnings, all authentication APIs migrated, tests pass.

---

## Research Findings

### Project Assessment

- **Current TFM**: net9.0 → **Target**: net10.0
- **SDK-style**: ✅ Yes
- **Issues**: 19 (1 Mandatory: TFM, 2 Potential: packages, 16 Potential: API changes)
- **Dependencies**: Model, GraphQL, Protocol, MQTT, Aspire.ServiceDefaults (all now net10.0 ✅)
- **Affected Files**: 1 file with issues (Program.cs)
- **LOC**: 690 total, ~60 LOC affected (authentication configuration block)

### Packages to Update
- `Microsoft.AspNetCore.Authentication.OpenIdConnect`: 9.0.5 → **10.0.8**
- `Microsoft.AspNetCore.OpenApi`: 9.0.5 → **10.0.8**
- `Scalar.AspNetCore`: 2.4.7 (✅ compatible, no update)
- `Serilog.AspNetCore`: 9.0.0 (✅ compatible, no update)

### API Changes: OpenIdConnect and JWT Tokens

**Issue 1**: `System.IdentityModel.Tokens.Jwt` namespace was removed in .NET Core.

**Affected Code (Program.cs, line 9)**:
```csharp
using System.IdentityModel.Tokens.Jwt;
```

Used on line 69:
```csharp
options.TokenValidationParameters.NameClaimType = JwtRegisteredClaimNames.Name;
```

**Fix**: Replace with `Microsoft.IdentityModel.JsonWebTokens` namespace:
```csharp
using Microsoft.IdentityModel.JsonWebTokens;
```

The `JwtRegisteredClaimNames` type still exists in `Microsoft.IdentityModel.JsonWebTokens`.

**Issue 2**: OpenIdConnectOptions properties are source incompatible — they likely changed signatures or behavior between 9.0.5 and 10.0.8. This will be detected at compile time.

### Execution Plan

1. Update TFM: net9.0 → net10.0 in DynSec.API.csproj
2. Update package versions (2 packages)
3. Fix namespace import: `System.IdentityModel.Tokens.Jwt` → `Microsoft.IdentityModel.JsonWebTokens`
4. Build and fix any remaining source incompatibilities in Program.cs
5. Validate all dependencies resolve correctly
6. Run tests (if any)
