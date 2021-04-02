using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web.GraphQL
{
    public class GraphQLMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDocumentWriter _writer;
        private readonly IDocumentExecuter _executor;
        private readonly GraphQLOptions _options;

        public GraphQLMiddleware(RequestDelegate next, IDocumentWriter writer, IDocumentExecuter executor, IOptions<GraphQLOptions> options)
        {
            _next = next;
            _writer = writer;
            _executor = executor;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext httpContext, ISchema schema)
        {
            if (httpContext.Request.Path.StartsWithSegments(_options.EndPoint) && string.Equals(httpContext.Request.Method, "POST", StringComparison.OrdinalIgnoreCase))
            {
                var request = await JsonSerializer
                                        .DeserializeAsync<GraphQLRequest>(
                                            httpContext.Request.Body,
                                            new JsonSerializerOptions
                                            {
                                                PropertyNameCaseInsensitive = true
                                            });

                var result = await _executor
                                .ExecuteAsync(doc =>
                                {
                                    doc.Schema = schema;
                                    doc.Query = request.Query;
                                    doc.Inputs = request.Variables.ToInputs();
                                }).ConfigureAwait(false);

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = 200;

                await _writer.WriteAsync(httpContext.Response.Body, result);
            }
            else
            {
                await _next(httpContext);
            }
        }
    }

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