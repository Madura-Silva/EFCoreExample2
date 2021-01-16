using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSample.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public string SKU { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }
}
