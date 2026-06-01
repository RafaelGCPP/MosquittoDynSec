# DynSec .NET 10 Upgrade Plan

## Overview

**Target**: Upgrade DynSec solution from .NET 9.0 (and 6.0) to .NET 10.0
**Scope**: 8 projects across 5 dependency levels; 3,639 LOC; 11 package updates + 1 security fix

**Strategy**: Bottom-Up (tier-by-tier, foundation to application)
- Each tier is upgraded, validated, and committed before moving to the next
- Validation includes full build and test runs after each tier
- Clear dependency ordering ensures no broken references

---

## Tasks

### 01-foundation-libraries: Upgrade core foundation projects (Model, MQTT, Web, ServiceDefaults)

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

### 02-protocol-layer: Upgrade Protocol library

DynSec.Protocol (790 LOC) depends only on DynSec.Model (now net10.0). Contains MQTT protocol abstractions and 2 identified API issues with TimeSpan.FromSeconds. Upgrade packages and fix source incompatibilities.

**Included work**:
- Update target framework to net10.0
- Update NuGet packages: Microsoft.Extensions.* packages
- Fix 3 instances of TimeSpan.FromSeconds incompatibility (API migration)
- Validate DynSec.Protocol builds successfully and depends only on updated Model
- Run unit tests

**Done when**: DynSec.Protocol targets net10.0, builds without errors or warnings, source incompatibilities resolved, tests pass.

---

### 03-graphql-layer: Upgrade GraphQL library

DynSec.GraphQL (184 LOC) depends on DynSec.Model and DynSec.Protocol (both now net10.0). Contains HotChocolate 15.1.5 integration (already compatible with net10.0). Straightforward upgrade.

**Included work**:
- Update target framework to net10.0
- Verify HotChocolate 15.1.5 compatibility (already confirmed compatible)
- Validate DynSec.GraphQL builds successfully against upgraded dependencies
- Run unit tests

**Done when**: DynSec.GraphQL targets net10.0, builds without errors or warnings against net10.0 dependencies.

---

### 04-api-layer: Upgrade API application

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

### 05-aspire-application: Upgrade Aspire orchestration app

DynSec.Aspire (21 LOC) depends on DynSec.Web and DynSec.API (both now net10.0). Orchestrates the entire application. Final step in upgrade.

**Included work**:
- Update target framework to net10.0
- Update NuGet packages: Aspire.Hosting.* packages
- Validate DynSec.Aspire builds successfully against all upgraded dependencies
- Full end-to-end validation: Aspire app can start and orchestrate all services

**Done when**: DynSec.Aspire targets net10.0, builds without errors or warnings, solution builds successfully, Aspire app validates all services.

---

### 06-final-validation: Full solution validation and language modernization

After all projects are upgraded, run final validation and optionally modernize C# language features to version 13 (latest supported by net10.0).

**Included work**:
- Build entire solution (all projects together)
- Run full test suite
- Verify no warnings or errors across all projects
- Optionally: update language version to latest (C# 13) and apply modern language features
- Commit final state

**Done when**: Solution builds successfully without warnings, all tests pass, C# language features are modernized (if applicable).

---

## Strategy Notes

- **Strict Tier Ordering**: Foundation → Protocol → GraphQL → API → Aspire. No skipping ahead.
- **Validation After Each Tier**: Each tier must build and validate before the next tier is touched.
- **Commit After Each Task**: Clear checkpoint after foundation, protocol, GraphQL, API, Aspire, final validation.
- **Breaking Changes**: Minimal expected. Primary concern: OpenIdConnect authentication APIs in DynSec.API.

