using Autofac;
using CoreSample.DBAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSample.Service
{
    public class ServiceDependancyRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DBAccessDependancyRegister>();
            builder.RegisterType<CustomerService>().As<ICustomerService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<OrderService>().As<IOrderService>();

        }
    }
}
