using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.GraphQL
{
    public class GameStoreQuery : ObjectGraphType
    {
        public GameStoreQuery(IRepository repository, IDataLoaderContextAccessor accessor)
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

            Field<ListGraphType<ItemType>, IReadOnlyCollection<Item>>()
                .Name("items")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddLoader("GetAllItems", repository.GetItems);
                    return loader.LoadAsync();
                });

            FieldAsync<ListGraphType<OrderType>, IReadOnlyCollection<Order>>(
                "orders", 
                resolve: ctx =>
                {
                    return repository.GetOrders();
                });

            FieldAsync<ListGraphType<CustomerType>, IReadOnlyCollection<Customer>>(
                "customers",
                resolve: ctx =>
                {
                    return repository.GetCustomers();
                });

            FieldAsync<ListGraphType<OrderItemType>, IReadOnlyCollection<OrderItem>>(
                "orderItem",
                resolve: ctx =>
                {
                    return repository.GetOrderItem();
                });
        }
    }
}
