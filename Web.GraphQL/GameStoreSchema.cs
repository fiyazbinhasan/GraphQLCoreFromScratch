using System;
using GraphQL.Types;
using GraphQL.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Web.GraphQL
{
    public class GameStoreSchema : Schema
    {
        public GameStoreSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<GameStoreQuery>();
            Mutation = serviceProvider.GetRequiredService<GameStoreMutation>();
        }
    }
}
