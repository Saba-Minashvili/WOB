using Microsoft.Extensions.DependencyInjection;
using Persistence.Mapper;

namespace Extensions
{
    public static class AddAutoMapper
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ObjectMapper));
        }
    }
}
