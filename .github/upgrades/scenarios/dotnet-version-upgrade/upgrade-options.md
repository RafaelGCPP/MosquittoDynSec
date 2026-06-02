# Upgrade Options - DynSec to .NET 10

## Upgrade Strategy

### Options

| Strategy | Description | Fit |
|----------|-------------|-----|
| **Bottom-Up** (selected) | Upgrade projects in dependency order: foundation libraries first → services → applications. Best for multi-layered solutions with clear dependency hierarchy. | ✅ **Recommended** — Your solution has 5 clear dependency levels (foundation → protocol → GraphQL → API → Aspire). Ensures each layer is validated before moving up. |
| All-at-Once | Upgrade entire solution simultaneously. Fast but higher risk if issues arise. | ⚠️ Alternative if you want speed over validation |
| Top-Down | Upgrade applications first, backtrack to libraries. Higher rebuild cycles. | ⚠️ Alternative if applications are priority |

**Selected**: Bottom-Up
**Rationale**: 5-level dependency hierarchy with clear ordering. Bottom-Up allows validating foundation libraries first (Model, MQTT, Web, ServiceDefaults) before moving to dependent layers (Protocol → GraphQL → API → Aspire).

---

## Package Management

### Options

| Approach | Description | Selection |
|----------|-------------|-----------|
| **Centralized (CPM)** | Use Central Package Management (Directory.Packages.props) for unified versioning | ⚠️ Optional enhancement (not required) |
| **Individual** (selected) | Keep current per-project PackageReference approach | ✅ **Recommended** — Current setup works fine, no .NET Framework projects. CPM is a nice-to-have modernization, not blocking. |

**Selected**: Individual

---

## Modernization (Optional)

### C# Language Version

| Option | Description | Selection |
|--------|-------------|-----------|
| **Auto-upgrade to latest supported** (selected) | Set C# version to latest supported by .NET 10 (currently C# 13) | ✅ **Recommended** — Take advantage of modern language features during upgrade |
| Keep current version | No language version change | ⚠️ Alternative if conserving C# features |

**Selected**: Auto-upgrade to latest supported

---

## Validation Strategy

### Approach

| When | Validation | Requirement |
|------|-----------|-------------|
| After each tier | Solution builds, tests pass | ✅ **Mandatory** |
| Final | Full solution build warning-free | ✅ **Mandatory** |

---

## Security Patch

| Package | Current | Target | Severity |
|---------|---------|--------|----------|
| OpenTelemetry.Exporter.OpenTelemetryProtocol | 1.12.0 | **1.15.3** | 🔴 **Critical** — Will be included |

---

## Summary

| Item | Decision |
|------|----------|
| **Strategy** | Bottom-Up (tier-by-tier validation) |
| **Package Management** | Individual (current approach) |
| **Language Version** | Auto-upgrade to C# 13 |
| **Target Framework** | .NET 10.0 (LTS) |
| **Security Patches** | Included (OpenTelemetry fix) |
| **Commit Strategy** | After Each Task |

