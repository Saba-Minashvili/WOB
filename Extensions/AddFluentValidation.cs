using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Middleware.Filters;
using Middleware.Validators;

namespace Extensions
{
    public static class AddFluentValidation
    {
        public static void ConfigureFilters(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter>();
            });
        }

        public static void ConfigureValidation(this IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssembly(typeof(RegisterUserDtoValidator).Assembly));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}
