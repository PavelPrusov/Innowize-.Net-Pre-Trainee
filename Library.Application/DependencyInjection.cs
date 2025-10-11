using Library.BusinessLogic.Interfaces;
using Library.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Library.BusinessLogic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            return services;
        }
    }
}
