using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSample.Infrastructure.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public string SKU { get; set; }
        public int NoOfItemsOrdered { get; set; }
    }
}
