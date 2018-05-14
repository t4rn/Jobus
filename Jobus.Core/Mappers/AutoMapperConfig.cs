using AutoMapper;

namespace Jobus.Core.Mappers
{
    public class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            IMapper mapperConfig = new MapperConfiguration(cfg =>
            {
            })
            .CreateMapper();

            mapperConfig.ConfigurationProvider.AssertConfigurationIsValid();

            return mapperConfig;
        }
    }
}
