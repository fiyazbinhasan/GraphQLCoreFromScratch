using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.GraphQL
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Tag { get; set; }
        public DateTime CreatedAt { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
