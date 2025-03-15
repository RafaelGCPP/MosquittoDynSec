using Arshid.Aspire.ApiDocs.Extensions;
using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var dynsecapi = builder.AddProject<Projects.DynSec_API>("DynSecApi")
    .WithEnvironment("ASPNETCORE_HTTPS_PORT", "7044")
    .WithEnvironment("FRONTEND_HTTPS_PORT", "4200")
    .WithExternalHttpEndpoints()
    .WithScalar();


//var dynsecfrontend=builder.AddNpmApp("angular", "..\\DynSec.Web")
//    .WithReference(dynsecapi)
//    .WithHttpsEndpoint(port:4200, targetPort:4200,name:"frontend",env: "PORT", isProxied:false)
//    .WithEnvironment("ASPNETCORE_HTTPS_PORT","7044")
//    .WithExternalHttpEndpoints()
//    .WaitFor(dynsecapi)
//    ;


builder.Build().Run();

