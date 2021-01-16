using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSample.Domain
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int NumberOfItems { get; set; }
    }
}
