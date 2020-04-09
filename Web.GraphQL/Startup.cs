using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Web.GraphQL
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapPost("/graphql", async context =>
                {

                    var request = await JsonSerializer
                                        .DeserializeAsync<GraphQLRequest>(
                                            context.Request.Body,
                                            new JsonSerializerOptions
                                            {
                                                PropertyNameCaseInsensitive = true
                                            });

                    var schema = new Schema { Query = new GameStoreQuery() };

                    var result = await new DocumentExecuter()
                                    .ExecuteAsync(doc =>
                                    {
                                        doc.Schema = schema;
                                        doc.Query = request.Query;
                                    }).ConfigureAwait(false);

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 200;

                    await new DocumentWriter(indent: true).WriteAsync(context.Response.Body, result);
                });
            });
        }
    }
}
