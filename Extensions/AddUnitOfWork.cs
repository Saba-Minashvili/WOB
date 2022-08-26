using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Extensions
{
    public static class AddUnitOfWork
    {
        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
        }
    }
}
