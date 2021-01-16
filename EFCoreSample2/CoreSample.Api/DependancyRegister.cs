using Autofac;
using AutofacSerilogIntegration;
using CoreSample.Infrastructure.Settings;
using CoreSample.Service;
using CoreSample.Variables;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSample.Api
{
    public class DependancyRegister :Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var configurations = GetAppConfigurations();
            builder.RegisterModule<ServiceDependancyRegister>();
            
            //register DB connection
            builder.Register(c => configurations).As<IConfigurations>().InstancePerLifetimeScope();

            //Register Serilog 
            builder.RegisterLogger();
        }


        //read DB connection string
        private Configurations GetAppConfigurations()
        {
            var configuration = new Configurations();
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            config.GetSection("App").Bind(configuration);
            return configuration;
        }

    }
}
