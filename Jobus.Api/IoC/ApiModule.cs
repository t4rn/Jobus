using Autofac;
using Jobus.Core.IoC;
using Jobus.DataAccess.IoC;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Jobus.Api.IoC
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<CoreModule>();

            builder.RegisterType<ActionContextAccessor>().As<IActionContextAccessor>().SingleInstance();
        }
    }
}
