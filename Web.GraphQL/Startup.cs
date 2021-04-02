using GraphQL;
using GraphQL.DataLoader;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
            services.AddTransient<ItemInputType>();
            services.AddTransient<ItemType>();
            services.AddTransient<CustomerType>();
            services.AddTransient<CustomerInputType>();
            services.AddTransient<OrderType>();
            services.AddTransient<OrderInputType>();
            services.AddTransient<OrderItemType>();
            services.AddTransient<OrderItemInputType>();

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
            services.AddDbContext<ApplicationDbContext>(options => options.UseLoggerFactory(loggerFactory).UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=GameStoreDb;Trusted_Connection=True;"), ServiceLifetime.Transient);
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