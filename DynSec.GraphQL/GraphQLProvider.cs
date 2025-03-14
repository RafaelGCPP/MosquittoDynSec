using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DynSec.GraphQL
{
    public static class GraphQLProvider
    {
        public static WebApplicationBuilder AddDynSecGraphQL(this WebApplicationBuilder builder)
        {

            var service = builder.Services.AddGraphQLServer()
                .AddQueryType<DynSecQuery>()
                .AddMutationType<DynSecMutation>()
                .AddErrorFilter<DynSecErrorFilter>();

            if (!builder.Environment.IsDevelopment())
            {
                service.DisableIntrospection();
            }

            return builder;
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
