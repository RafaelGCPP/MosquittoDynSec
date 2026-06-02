using DynSec.Protocol.Exceptions;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DynSec.GraphQL;

public static class GraphQLProvider
{
    public static WebApplicationBuilder AddDynSecGraphQL(this WebApplicationBuilder builder)
    {

        var service = builder.Services.AddGraphQLServer()
            .AddQueryType<DynSecQuery>()
            .AddMutationType<DynSecMutation>()
            .AddErrorFilter(ErrorFilter);

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

    static IError ErrorFilter(IError error)
    {
        ErrorBuilder eb = ErrorBuilder.FromError(error);

        eb = error.Exception switch
        {
            DynSecProtocolInvalidParameterException ex => eb.SetMessage(ex.Message).SetCode("INVALID_PARAMETER"),
            DynSecProtocolNotFoundException ex => eb.SetMessage(ex.Message).SetCode("NOT_FOUND"),
            DynSecProtocolDuplicatedException ex => eb.SetMessage(ex.Message).SetCode("DUPLICATED"),
            DynSecProtocolTimeoutException ex => eb.SetMessage(ex.Message).SetCode("MQTT_TIMEOUT"),
            DynSecProtocolException ex => eb.SetMessage(ex.Message).SetCode("DYNAMIC_SECURITY"),
            _ => eb
        };

        return eb.Build();
    }
}



