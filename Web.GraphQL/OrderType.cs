using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.GraphQL
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType(IRepository repository)
        {
            Field(o => o.Tag);
            Field(o => o.CreatedAt);

            FieldAsync<CustomerType, Customer>("customer",
                resolve: ctx =>
                {
                    return repository.GetCustomerById(ctx.Source.CustomerId);
                });
        }
    }
}
