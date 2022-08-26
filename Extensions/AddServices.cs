using EmailSender.Services;
using EmailSender.Services.Abstraction;
using Encoder;
using Encoder.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Authentication;
using Persistence.Authentication.Abstraction;
using Services;
using Services.Abstractions;

namespace Extensions
{
    public static class AddServices
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IJwtAuthenticationService, JwtAuthenticationService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEncodeService, EncodeService>();
        }
    }
}
