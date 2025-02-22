using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DynSec.GraphQL
{
    public static class GraphQLProvider
    {
        public static IServiceCollection AddDynSecGraphQL(this IServiceCollection services)
        {

            var hcbuilder = services.AddGraphQLServer()
                .AddQueryType<DynSecQuery>()
                .AddMutationType<DynSecMutation>()
                .AddErrorFilter<DynSecErrorFilter>();

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
