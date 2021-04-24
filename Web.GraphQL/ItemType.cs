using GraphQL.Relay.Types;
using System;
using System.Threading.Tasks;

namespace Web.GraphQL
{
    public class ItemType : AsyncNodeGraphType<Item>
    {
        private readonly IRepository _repository;

        public ItemType(IRepository repository)
        {
            _repository = repository;

            Id(p => p.Id);

            Field(i => i.Tag);
            Field(i => i.Title);
            Field(i => i.Price);
        }

        public override async Task<Item> GetById(string id)
        {
            return await _repository.GetItemById(Convert.ToInt32(id));
        }
    }
}