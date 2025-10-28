using Library.DataAccess.Data;
using Library.DataAccess.Data.Seed;
using Library.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Library.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<LibraryListContext>();

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IAuthorRepository,AuthorRepository>();
            services.AddScoped<IBookRepository,BookRepository>();

            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
        public static async Task InitializeDatabaseAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
            await SeedData.InitializeAsync(context);
        }
    }
}