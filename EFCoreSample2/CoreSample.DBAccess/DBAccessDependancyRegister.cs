using Autofac;
using AutofacSerilogIntegration;
using CoreSample.DBAccess.Queries;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CoreSample.DBAccess
{
    public class DBAccessDependancyRegister: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CoreSampleDBContext>().As<ICoreSampleDBContext>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerQuery2>().As<ICustomerQuery>();
            builder.RegisterType<ProductQuery2>().As<IProductQuery>();
            builder.RegisterType<OrderQuery2>().As<IOrderQuery>();

            #region MediatR Injection
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.RegisterAssemblyTypes(typeof(DBAccessDependancyRegister).GetTypeInfo().Assembly).AsImplementedInterfaces();
            #endregion MediatR

            #region Serilog
            //https://stackoverflow.com/questions/29756043/serilog-with-autofac
            //https://github.com/serilog/serilog-sinks-file
            //https://www.codewithmukesh.com/blog/serilog-in-aspnet-core-3-1/
            //var assembly = Assembly.GetExecutingAssembly();
            //Log.Logger = new LoggerConfiguration()
            //    .WriteTo.File($".\\Log\\log.txt",rollingInterval: RollingInterval.Day)
            //    .CreateLogger();
            //builder.RegisterLogger();
            #endregion
        }
    }
}
