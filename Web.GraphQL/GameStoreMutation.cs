using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.GraphQL
{
    public class GameStoreMutation : ObjectGraphType
    {
        public GameStoreMutation(IRepository repository)
        {
            FieldAsync<ItemType>(
                "createItem",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ItemInputType>> { Name = "item" }
                ),
                resolve: async context =>
                {
                    var item = context.GetArgument<Item>("item");
                    return await repository.AddItem(item);
                });
        }
    }
}
