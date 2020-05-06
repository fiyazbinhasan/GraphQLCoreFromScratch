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
        public GameStoreQuery()
        {
            Field<StringGraphType>(
                name: "name",
                resolve: context => "Steam"
            );

            Field<ItemType>(
                "item",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "tag" }),
                resolve: context =>
                {
                    var tag = context.GetArgument<string>("tag");
                    return new DataSource().GetItemByTag(tag);
                }
            );
        }
    }
}
