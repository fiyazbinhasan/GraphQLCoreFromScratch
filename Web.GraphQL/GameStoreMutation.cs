using GraphQL;
using GraphQL.Relay.Types;
using GraphQL.Types;

namespace Web.GraphQL
{
    public class GameStoreMutation : MutationGraphType
    {
        public GameStoreMutation(IRepository repository)
        {
            Mutation<CreateItemInput, CreateItemPayload>("createItem");

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