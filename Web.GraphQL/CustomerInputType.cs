using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
