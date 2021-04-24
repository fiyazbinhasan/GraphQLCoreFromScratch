using GraphQL;
using GraphQL.Relay.Types;
using GraphQL.Types.Relay;
using GraphQL.Types.Relay.DataObjects;
using System.Threading.Tasks;

namespace Web.GraphQL
{
    public class CreateItemPayload : MutationPayloadGraphType<object, Task<object>>
    {
        private readonly IRepository _repository;

        public CreateItemPayload(IRepository repository)
        {
            _repository = repository;

            Field<EdgeType<ItemType>>("itemEdge");
        }

        public override async Task<object> MutateAndGetPayload(MutationInputs inputs, IResolveFieldContext<object> context)
        {
            string tag = inputs.Get<string>("tag");
            string title = inputs.Get<string>("title");
            decimal price = inputs.Get<decimal>("price");

            Item item = await _repository.AddItem(new Item { Tag = tag, Title = title, Price = price });

            return new
            {
                ItemEdge = new Edge<Item>
                {
                    Node = item,
                    Cursor = ConnectionUtils.CursorForObjectInConnection(await _repository.GetItems(), item)
                },
            };
        }
    }
}