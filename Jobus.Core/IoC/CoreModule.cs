using Autofac;
using Jobus.Core.Services;
using System.Linq;
using System.Reflection;

namespace Jobus.Core.IoC
{
    public class CoreModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var svcAssembly = typeof(CoreModule).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(svcAssembly)
                .Where(x => x.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
