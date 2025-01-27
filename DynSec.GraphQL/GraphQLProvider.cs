using HotChocolate.Resolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DynSec.GraphQL
{
    public static class GraphQLProvider
    {
        public static IServiceCollection AddDynSecGraphQL(this IServiceCollection services)
        {

            var hcbuilder = services.AddGraphQLServer()
                .AddQueryType<DynSecQuery>();

            return services;
        }

        public static IApplicationBuilder MapDynSecGraphQL(this IApplicationBuilder app)
        {

            app.UseEndpoints(endpoints =>
            {                
                endpoints.MapGraphQL();
            });
            return app;
        }
    }
}
