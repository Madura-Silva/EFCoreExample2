using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSample.Infrastructure.Settings
{
    public interface IConfigurations
    {
        string DBConnection { get; set; }

        string DBConnection2 { get; set; }
    }
}
