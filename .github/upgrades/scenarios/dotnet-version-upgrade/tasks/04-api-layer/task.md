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
