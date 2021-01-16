using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSample.Infrastructure.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? OrderedDate { get; set; }
        public string OrderAddress { get; set; }
        public string BillingAddress { get; set; }
        public CustomerDTO Customer { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
