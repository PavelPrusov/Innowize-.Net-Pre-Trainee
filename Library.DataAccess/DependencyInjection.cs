using Library.DataAccess.Data;
using Library.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<LibraryListContext>();

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}