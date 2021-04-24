using GraphQL.Relay.Types;
using GraphQL.Types;

namespace Web.GraphQL
{
    public class CreateItemInput : MutationInputGraphType
    {
        public CreateItemInput()
        {
            Name = "CreateItemInput";
            Field<NonNullGraphType<StringGraphType>>("tag");
            Field<NonNullGraphType<StringGraphType>>("title");
            Field<NonNullGraphType<DecimalGraphType>>("price");
        }
    }
}