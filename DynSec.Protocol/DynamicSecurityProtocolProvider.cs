using DynSec.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DynSec.Protocol
{
    public static class DynamicSecurityProtocolProvider
    {
        public static IServiceCollection AddDynamicSecurityProtocol (this IServiceCollection services)
        {
            services.AddSingleton<IDynamicSecurityHandler, DynamicSecurityHandler>();
            services.AddSingleton<ICLientsService, ClientsService>();
            services.AddSingleton<IRolesService, RolesService>();
            return services;
        }
    }
}
