using GraphQL.Types;
using System.Collections.Generic;

namespace Web.GraphQL
{
    public class CustomerType : ObjectGraphType<Customer>
    {
        public CustomerType(IRepository repository)
        {
            Field(c => c.Name);
            Field(c => c.BillingAddress);

            FieldAsync<ListGraphType<OrderType>, IReadOnlyCollection<Order>>(
                "orders",
                resolve: ctx =>
                {
                    return repository.GetOrdersByCustomerId(ctx.Source.CustomerId);
                });
        }
    }
}