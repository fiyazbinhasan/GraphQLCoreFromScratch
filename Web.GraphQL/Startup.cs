using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Execution;
using GraphQL.NewtonsoftJson;
using GraphQL.Relay.Types;
using GraphQL.Server;
using GraphQL.Types;
using GraphQL.Types.Relay;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Web.GraphQL
{
    public class Startup
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<DataLoaderDocumentListener>();
            services.AddTransient<ISchema, GameStoreSchema>();
            services.AddTransient<GameStoreQuery>();
            services.AddTransient<GameStoreMutation>();
            services.AddTransient<CreateItemInput>();
            services.AddTransient<CreateItemPayload>();
            services.AddTransient<ItemType>();
            services.AddTransient<CustomerType>();
            services.AddTransient<CustomerInputType>();
            services.AddTransient<OrderType>();
            services.AddTransient<OrderInputType>();
            services.AddTransient<OrderItemType>();
            services.AddTransient<OrderItemInputType>();

            services.AddTransient(typeof(ConnectionType<>))
                .AddTransient(typeof(EdgeType<>))
                .AddTransient<NodeInterface>()
                .AddTransient<PageInfoType>();

            services.AddGraphQL(options =>
            {
                options.EndPoint = "/graphql";
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddTransient<IRepository, Repository>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseLoggerFactory(loggerFactory).UseInMemoryDatabase(@"GameStoreDb"), ServiceLifetime.Transient);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseGraphQL();
        }
    }
}