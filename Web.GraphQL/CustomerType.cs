using GraphQL.DataLoader;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.GraphQL
{
    public class CustomerType : ObjectGraphType<Customer>
    {
        public CustomerType(IRepository repository, IDataLoaderContextAccessor accessor)
        {
            Field(c => c.Name);
            Field(c => c.BillingAddress);

            Field<ListGraphType<OrderType>, IEnumerable<Order>>()
                .Name("orders")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddCollectionBatchLoader<int, Order>("GetOrdersByCustomerId", repository.GetOrdersByCustomerId);
                    return loader.LoadAsync(ctx.Source.CustomerId);
                });
        }
    }
}
