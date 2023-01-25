using AutoMapper;
using MediscreenAPI.Model;
using MediscreenAPI.Services;

namespace PoseidonApi.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            MapperConfiguration mapperConfig = new(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<HistoryDatabaseSettings>(configuration.GetSection("HistoryDatabase"));
            services.AddSingleton<HistoryService>();
            services.AddSingleton<IReadHistoryService>(provider => provider.GetService<HistoryService>());
        }
    }
}
