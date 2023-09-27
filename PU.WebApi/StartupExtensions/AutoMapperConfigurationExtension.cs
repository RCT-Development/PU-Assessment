namespace PU.WebApi.StartupExtensions
{
    public static class AutoMapperConfigurationExtension
    {
        public static void AddCustomAutomapperConfiguration(this IServiceCollection services)
        {
            var mapperProfileList = new Type[]
            {
                typeof(DataAccess.Config.MapperProfile),
                typeof(Config.MapperProfile)
            };

            services.AddAutoMapper(mapperProfileList);
        }
    }
}
