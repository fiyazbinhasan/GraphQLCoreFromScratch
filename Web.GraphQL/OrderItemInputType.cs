using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.GraphQL
{
    public class OrderItemInputType : InputObjectGraphType
    {
        public OrderItemInputType()
        {
            Name = "OrderItemInput";
            Field<NonNullGraphType<IntGraphType>>("quantity");
            Field<NonNullGraphType<IntGraphType>>("itemId");
            Field<NonNullGraphType<IntGraphType>>("orderId");
        }
    }
}
