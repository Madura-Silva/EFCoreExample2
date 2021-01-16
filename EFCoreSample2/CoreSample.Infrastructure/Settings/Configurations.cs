using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSample.Infrastructure.Settings
{
    public class Configurations : IConfigurations
    {
        public string DBConnection { get; set; }
        public string DBConnection2 { get; set; }
    }
}
