using EmailSender.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Extensions
{
    public static class AddEmailConnection
    {
        public static void ConfigureEmailConnectionSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
            services.AddScoped<IUrlHelper>(o =>
            {
                var actionContext = o.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = o.GetRequiredService<IUrlHelperFactory>();

#pragma warning disable CS8604 // Possible null reference argument.
                return factory.GetUrlHelper(actionContext);
#pragma warning restore CS8604 // Possible null reference argument.
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton(configuration.GetSection("EmailConfiguration"));
        }
    }
}
