using Autofac;
using Jobus.DataAccess.Repositories;
using System.Linq;
using System.Reflection;

namespace Jobus.DataAccess.IoC
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var repoAssembly = typeof(RepositoryModule).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(repoAssembly)
                .Where(x => x.IsAssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
