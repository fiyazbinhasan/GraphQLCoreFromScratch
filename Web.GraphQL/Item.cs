﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.GraphQL
{
    public class Item
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
