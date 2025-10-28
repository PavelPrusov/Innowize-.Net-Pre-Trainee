using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Data.Seed
{
    public static class SeedData
    {
        public static async Task InitializeAsync(LibraryDbContext context)
        {
            if (await context.Authors.AnyAsync())
                return;
            if (await context.Books.AnyAsync())
                return;

            await SeedAuthorsAsync(context);
            await SeedBooksAsync(context);
        }

        private static async Task SeedAuthorsAsync(LibraryDbContext context)
        {
            var authors = new[]
            {
                new Author { Name = "Stephen King", DateOfBirth = new DateOnly(1947, 9, 21) },
                new Author { Name = "J.K. Rowling", DateOfBirth = new DateOnly(1965, 7, 31) },
                new Author { Name = "J.R.R. Tolkien", DateOfBirth = new DateOnly(1892, 1, 3) }
            };

            await context.Authors.AddRangeAsync(authors);
            await context.SaveChangesAsync();
        }

        private static async Task SeedBooksAsync(LibraryDbContext context)
        {
            var authors = await context.Authors.ToListAsync();

            var books = new[]
            {
                new Book { Title = "The Shining", PublishedYear = 1977, AuthorId = authors[0].Id },
                new Book { Title = "It", PublishedYear = 1986, AuthorId = authors[0].Id },
                new Book { Title = "Harry Potter and the Philosopher's Stone", PublishedYear = 1997, AuthorId = authors[1].Id },
                new Book { Title = "Harry Potter and the Prisoner of Azkaban", PublishedYear = 1999, AuthorId = authors[1].Id },
                new Book { Title = "The Hobbit", PublishedYear = 1937, AuthorId = authors[5].Id },
                new Book { Title = "The Fellowship of the Ring", PublishedYear = 1954, AuthorId = authors[5].Id }
            };

            await context.Books.AddRangeAsync(books);
            await context.SaveChangesAsync();
        }
    }
}
