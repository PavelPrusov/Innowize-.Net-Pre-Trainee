using FluentValidation;
using Library.BusinessLogic.DTO.Author;
using Library.BusinessLogic.DTO.Book;
using Library.BusinessLogic.Interfaces;
using Library.BusinessLogic.Services;
using Library.BusinessLogic.Validators.Author;
using Library.BusinessLogic.Validators.Book;
using Microsoft.Extensions.DependencyInjection;

namespace Library.BusinessLogic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IValidator<CreateAuthorDto>, CreateAuthorValidator>();
            services.AddScoped<IValidator<UpdateAuthorDto>, UpdateAuthorValidator>();

            services.AddScoped<IValidator<CreateBookDto>, CreateBookValidator>();
            services.AddScoped<IValidator<UpdateBookDto>, UpdateBookValidator>();

            return services;
        }
    }
}
