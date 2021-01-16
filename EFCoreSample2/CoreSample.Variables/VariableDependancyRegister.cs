using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSample.Variables
{
    public class VariableDependancyRegister: Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationReader>().SingleInstance();
        }
    }
}
