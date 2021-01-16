using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace CoreSample.Variables
{
    public sealed class ConfigurationReader
    {
        private static ConfigurationReader instance = null;
        private static readonly object padlock = new object();

 

        #region Properties
        public string DBConnectionString { get; set; }
        #endregion


        public static ConfigurationReader Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ConfigurationReader();
                    }
                    return instance;
                }
            }
        }

        

        public void SetConfigValues(IConfiguration configuration)
        {
            DBConnectionString = configuration.GetConnectionString("DBConnection");
        }
    }
}
