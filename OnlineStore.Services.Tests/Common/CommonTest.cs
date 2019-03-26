using AutoMapper;
using OnlineStore.Web.Mapping;

namespace OnlineStore.Services.Tests.Common
{
    public static class CommonTest
    {
        public static IMapper GetAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            var mapper = config.CreateMapper();

            return mapper;
        }
    }
}
