using System;
using GraphQL.Types;
using GraphQL.Utilities;

namespace Web.GraphQL
{
    public class GameStoreSchema : Schema
    {
        public GameStoreSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<GameStoreQuery>();
        }
    }
}
