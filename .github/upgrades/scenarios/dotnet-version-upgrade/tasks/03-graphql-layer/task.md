# 03-graphql-layer: Upgrade GraphQL library

DynSec.GraphQL (184 LOC) depends on DynSec.Model and DynSec.Protocol (both now net10.0). Contains HotChocolate 15.1.5 integration (already compatible with net10.0). Straightforward upgrade.

**Included work**:
- Update target framework to net10.0
- Verify HotChocolate 15.1.5 compatibility (already confirmed compatible)
- Validate DynSec.GraphQL builds successfully against upgraded dependencies
- Run unit tests

**Done when**: DynSec.GraphQL targets net10.0, builds without errors or warnings against net10.0 dependencies.
