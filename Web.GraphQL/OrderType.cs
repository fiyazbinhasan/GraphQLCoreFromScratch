using GraphQL.DataLoader;
using GraphQL.Types;

namespace Web.GraphQL
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType(IRepository repository, IDataLoaderContextAccessor accessor)
        {
            Field(o => o.Tag);
            Field(o => o.CreatedAt);

            Field<CustomerType, Customer>()
                .Name("customer")
                .ResolveAsync(ctx =>
                {
                    var loader = accessor.Context.GetOrAddBatchLoader<int, Customer>("GetCustomersById", repository.GetCustomersById);
                    return loader.LoadAsync(ctx.Source.CustomerId);
                });
        }
    }
}