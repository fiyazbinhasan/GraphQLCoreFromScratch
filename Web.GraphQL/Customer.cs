using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.GraphQL
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string BillingAddress { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
