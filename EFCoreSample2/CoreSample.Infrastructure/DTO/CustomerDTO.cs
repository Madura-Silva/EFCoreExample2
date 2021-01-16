using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSample.Infrastructure.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }
}
