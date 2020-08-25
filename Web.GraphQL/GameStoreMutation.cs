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

            FieldAsync<CustomerType>(
               "createCustomer",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<CustomerInputType>> { Name = "customer" }
               ),
               resolve: async context =>
               {
                   var customer = context.GetArgument<Customer>("customer");
                   return await repository.AddCustomer(customer);
               });

            FieldAsync<OrderType>(
               "createOrder",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<OrderInputType>> { Name = "order" }
               ),
               resolve: async context =>
               {
                   var order = context.GetArgument<Order>("order");
                   return await repository.AddOrder(order);
               });

            FieldAsync<OrderItemType>(
                "addOrderItem",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<OrderItemInputType>> { Name = "orderItem" }
                ),
                resolve: async ctx =>
                {
                    var orderItem = ctx.GetArgument<OrderItem>("orderItem");
                    return await repository.AddOrderItem(orderItem);
                });
        }
    }
}
