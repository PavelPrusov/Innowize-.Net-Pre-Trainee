using Library.DataAccess.Data;
using Library.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Library.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddSingleton<LibraryDbContext>();

            services.AddScoped(typeof(IRepository<>), typeof(InMemoryRepository<>));

            return services;
        }
    }
}