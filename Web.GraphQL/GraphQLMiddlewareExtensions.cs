using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Web.GraphQL
{
    public static class GraphQLMiddlewareExtensions
    {
        public static IApplicationBuilder UseGraphQL(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GraphQLMiddleware>();
        }

        public static IServiceCollection AddGraphQL(this IServiceCollection services, Action<GraphQLOptions> action)
        {
            return services.Configure(action);
        }
    }
}
