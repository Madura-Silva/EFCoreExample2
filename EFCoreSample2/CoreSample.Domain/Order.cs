using System;
using System.Collections.Generic;

namespace CoreSample.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? OrderedDate { get; set; }
        public string OrderAddress { get; set; }
        public string BillingAddress { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }

    }
}