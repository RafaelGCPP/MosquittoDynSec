# 🔐 Mosquitto Dynamic Security Management

[![.NET 10](https://img.shields.io/badge/.NET-10.0-purple)](https://dotnet.microsoft.com/)
[![Angular 20](https://img.shields.io/badge/Angular-20-red)](https://angular.io/)
[![HotChocolate 16](https://img.shields.io/badge/HotChocolate-16-e535ab)](https://chillicream.com/docs/hotchocolate)
[![License](https://img.shields.io/badge/License-GPL--3.0-blue.svg)](LICENSE.txt)

A modern web application for managing the **Eclipse Mosquitto MQTT broker's Dynamic Security plugin**. This solution replaces cumbersome raw MQTT message handling with an intuitive GraphQL API and a sleek Angular frontend, making client, role, and group administration a breeze for system administrators.

---

## 📋 Table of Contents

- [Features](#-features)
- [Architecture](#-architecture)
- [Projects in this Solution](#-projects-in-this-solution)
- [Getting Started](#-getting-started)
  - [Prerequisites](#prerequisites)
  - [Running with .NET Aspire](#running-with-net-aspire)
  - [Running with Docker](#running-with-docker)
- [Configuration](#-configuration)
- [API Documentation](#-api-documentation)
- [Technology Stack](#-technology-stack)
- [Contributing](#-contributing)
- [License](#-license)

---

## ✨ Features

- **GraphQL API** powered by HotChocolate 16 for flexible, efficient data queries
- **Modern Angular 20** frontend with Material Design components
- **Real-time MQTT communication** via MQTTnet for managing Mosquitto Dynamic Security
- **OpenID Connect authentication** with JWT token validation
- **Distributed application orchestration** using .NET Aspire
- **Observability** with OpenTelemetry integration (logs, metrics, traces)
- **Custom error handling** with domain-specific GraphQL error codes
- **Docker support** for containerized deployments

---

## 🏗️ Architecture

```
┌─────────────────┐      GraphQL/HTTP       ┌──────────────────┐
│  Angular 20 SPA │ ◄──────────────────────► │  DynSec.API      │
│  (DynSec.Web)   │                          │  (.NET 10)       │
└─────────────────┘                          └────────┬─────────┘
                                                      │
                                                      │ Uses
                                                      ▼
                                             ┌──────────────────┐
                                             │ DynSec.GraphQL   │
                                             │ (HotChocolate)   │
                                             └────────┬─────────┘
                                                      │
                                            ┌─────────┼─────────┐
                                            ▼         ▼         ▼
                                    ┌──────────┬──────────┬──────────┐
                                    │ Protocol │  Model   │   MQTT   │
                                    └─────┬────┴──────────┴────┬─────┘
                                          │                     │
                                          └──────────┬──────────┘
                                                     ▼
                                          ┌──────────────────────┐
                                          │ Eclipse Mosquitto    │
                                          │ (Dynamic Security)   │
                                          └──────────────────────┘
```

---

## 📦 Projects in this Solution

### **DynSec.API**
**Backend Web API** built with **ASP.NET Core 10**
- Hosts the GraphQL endpoint via HotChocolate 16
- OpenID Connect authentication with Microsoft Identity
- Dependency injection container for services (logging, MQTT, GraphQL)
- OpenTelemetry integration for distributed tracing

### **DynSec.GraphQL**
**GraphQL schema and resolvers** powered by **HotChocolate 16**
- Query and mutation types for Dynamic Security entities
- Custom error filter for domain exceptions (`INVALID_PARAMETER`, `NOT_FOUND`, `DUPLICATED`, `MQTT_TIMEOUT`)
- Introspection disabled in production for security

### **DynSec.Protocol**
**Core MQTT communication engine**
- Implements pub-sub pattern for Dynamic Security RPC protocol
- Serializes/deserializes JSON payloads to domain models
- Handles timeouts and command execution logic
- Built on top of `DynSec.MQTT` and `DynSec.Model`

### **DynSec.Model**
**Data models and DTOs**
- POCOs for clients, roles, groups, ACLs
- JSON serialization/deserialization for Mosquitto's Dynamic Security plugin
- Shared across all layers (Protocol, GraphQL, API)

### **DynSec.MQTT**
**MQTTnet wrapper library**
- Simplifies MQTT client setup (TLS, WebSockets, credentials)
- Reduces boilerplate for broker connections
- Configurable via `appsettings.json`

### **DynSec.Web**
**Angular 20 SPA** with standalone components
- Material Design UI for managing clients, roles, groups
- Apollo Client 4.x for GraphQL communication
- TypeScript 5.8, RxJS 7.8
- Runs on HTTPS via Angular CLI dev server

### **DynSec.Aspire**
**.NET Aspire App Host** for local development orchestration
- Launches API and Web projects with service discovery
- Aspire Dashboard for monitoring (logs, metrics, traces)
- Pre-configured with Scalar API documentation

### **DynSec.Aspire.ServiceDefaults**
**Shared Aspire configuration**
- OpenTelemetry exporters (OTLP protocol)
- HTTP resilience and service discovery
- Common telemetry settings for distributed apps

---

## 🚀 Getting Started

### Prerequisites

- **.NET 10 SDK** ([Download](https://dotnet.microsoft.com/download/dotnet/10.0))
- **Node.js 20+** and **Yarn 4.9+** ([Download Node](https://nodejs.org/))
- **Eclipse Mosquitto** with Dynamic Security plugin enabled
- **(Optional) Docker** for containerized deployment

### Running with .NET Aspire

1. **Clone the repository**
   ```bash
   git clone https://github.com/RafaelGCPP/MosquittoDynSec.git
   cd MosquittoDynSec
   ```

2. **Configure MQTT connection**
   Edit `DynSec.API/appsettings.json`:
   ```json
   "MQTT": {
     "ClientId": "DynSec.API",
     "Port": 8883,
     "Username": "admin",
     "Password": "your-password",
     "Host": "your-mosquitto-broker",
     "WebSockets": false,
     "Tls": true
   }
   ```

3. **Run the Aspire App Host**
   ```bash
   cd DynSec.Aspire
   dotnet run
   ```

4. **Access the applications**
   - **Aspire Dashboard**: `https://localhost:17044` (check terminal output for exact port)
   - **API (Scalar Docs)**: `https://localhost:7044/scalar/v1`
   - **GraphQL Playground**: `https://localhost:7044/graphql`
   - **Angular Frontend**: `https://localhost:4200`

### Running with Docker

1. **Build the Docker image**
   ```bash
   docker build -t dynsec-app .
   ```

2. **Run the container**
   ```bash
   docker run -d \
     -p 8080:8080 \
     -e MQTT__Host=your-mosquitto-broker \
     -e MQTT__Password=your-password \
     --name dynsec \
     dynsec-app
   ```

3. **Access the app**: `http://localhost:8080`

---

## ⚙️ Configuration

### MQTT Settings (`appsettings.json`)

| Key                | Description                          | Default       |
|--------------------|--------------------------------------|---------------|
| `MQTT:Host`        | Mosquitto broker hostname/IP         | `localhost`   |
| `MQTT:Port`        | MQTT(S) port                         | `8883`        |
| `MQTT:ClientId`    | Unique client identifier             | `DynSec.API`  |
| `MQTT:Username`    | MQTT username                        | `admin`       |
| `MQTT:Password`    | MQTT password                        | *(required)*  |
| `MQTT:Tls`         | Enable TLS/SSL                       | `true`        |
| `MQTT:WebSockets`  | Use MQTT over WebSockets             | `false`       |

### Authentication

The API uses **OpenID Connect**. Configure your identity provider in `DynSec.API/Program.cs`:
```csharp
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddOpenIdConnect(options => {
        options.Authority = "https://your-identity-provider";
        options.ClientId = "your-client-id";
        // ... additional options
    });
```

---

## 📚 API Documentation

### GraphQL Endpoint
- **URL**: `https://localhost:7044/graphql`
- **Introspection**: Enabled in `Development` environment only

### Sample Query
```graphql
query GetClients {
  clients {
    username
    textName
    roles {
      rolename
    }
  }
}
```

### Sample Mutation
```graphql
mutation CreateClient($input: CreateClientInput!) {
  createClient(input: $input) {
    username
    textName
  }
}
```

### Error Codes

| Code                 | Description                                    |
|----------------------|------------------------------------------------|
| `INVALID_PARAMETER`  | Invalid input parameter                        |
| `NOT_FOUND`          | Requested entity not found                     |
| `DUPLICATED`         | Entity already exists                          |
| `MQTT_TIMEOUT`       | MQTT command timed out (check broker health)   |
| `DYNAMIC_SECURITY`   | General Dynamic Security plugin error          |

---

## 🛠️ Technology Stack

### Backend
- **.NET 10** - Modern C# runtime
- **HotChocolate 16** - GraphQL server
- **MQTTnet** - MQTT client library
- **Serilog + OpenTelemetry** - Structured logging & telemetry

### Frontend
- **Angular 20** - Standalone components, signals
- **Apollo Client 5** - GraphQL client
- **Angular Material 20** - UI components
- **TypeScript 5.8** - Type-safe JavaScript

### DevOps
- **.NET Aspire** - Local orchestration & observability
- **Docker** - Containerization
- **GitHub Actions** - CI/CD (see `.github/workflows`)

---

## 🤝 Contributing

Contributions are welcome! Please:
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

## 📄 License

This project is licensed under the **GNU General Public License v3.0** - see the [LICENSE.txt](LICENSE.txt) file for details.

---

## 🙏 Acknowledgments

- [Eclipse Mosquitto](https://mosquitto.org/) - The best open-source MQTT broker
- [HotChocolate](https://chillicream.com/) - Amazing GraphQL server for .NET
- [Angular Team](https://angular.io/) - For the incredible frontend framework

---

**Made by [Rafael Pinto](https://github.com/RafaelGCPP)**


