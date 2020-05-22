using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.GraphQL
{
    public class GameStoreQuery : ObjectGraphType
    {
        public GameStoreQuery(IRepository repository)
        {
            Field<StringGraphType>(
                name: "name",
                resolve: context => "Steam"
            );

            FieldAsync<ItemType>(
                "item",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "tag" }),
                resolve: async context =>
                {
                    var tag = context.GetArgument<string>("tag");
                    return await repository.GetItemByTag(tag);
                }
            );

            FieldAsync<ListGraphType<ItemType>>(
                "items",
                resolve: async context =>
                {
                    return await repository.GetItems();
                }
            );
        }
    }
}
