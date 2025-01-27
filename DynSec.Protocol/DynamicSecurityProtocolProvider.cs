using DynSec.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynSec.Protocol
{
    public static class DynamicSecurityProtocolProvider
    {
        public static IServiceCollection AddDynamicSecurityProtocol (this IServiceCollection services)
        {
            services.AddSingleton<IDynamicSecurityHandler, DynamicSecurityHandler>();
            services.AddSingleton<ICLientsService, ClientsService>();
            return services;
        }
    }
}
