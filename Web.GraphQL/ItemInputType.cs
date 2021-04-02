using GraphQL.Types;

namespace Web.GraphQL
{
    public class ItemInputType : InputObjectGraphType
    {
        public ItemInputType()
        {
            Name = "ItemInput";
            Field<NonNullGraphType<StringGraphType>>("tag");
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<NonNullGraphType<DecimalGraphType>>("price");
        }
    }
}