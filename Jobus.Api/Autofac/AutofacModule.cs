using Autofac;
using Jobus.Core.Services.Cache;
using Jobus.Core.Services.WsClients;
using Jobus.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Jobus.Api.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ActionContextAccessor>().As<IActionContextAccessor>().SingleInstance();
            builder.RegisterType<WsClientService>().As<IWsClientService>();
            builder.RegisterType<WsClientRepository>().As<IWsClientRepository>();
            builder.RegisterType<CacheService>().As<ICacheService>();

            base.Load(builder);
        }
    }
}
