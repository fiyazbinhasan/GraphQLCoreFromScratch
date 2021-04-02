using GraphQL.Types;

namespace Web.GraphQL
{
    public class ItemType : ObjectGraphType<Item>
    {
        public ItemType()
        {
            Field(i => i.Tag);
            Field(i => i.Title);
            Field(i => i.Price);
        }
    }
}