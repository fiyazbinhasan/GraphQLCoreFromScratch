using GraphQL.Types;

namespace Web.GraphQL
{
    public class CustomerInputType : InputObjectGraphType
    {
        public CustomerInputType()
        {
            Name = "CustomerInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("billingAddress");
        }
    }
}