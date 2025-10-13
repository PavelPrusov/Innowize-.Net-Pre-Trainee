using Library.Domain.Entities;
using System.Collections;

namespace Library.DataAccess.Data
{
    public class LibraryDbContext
    {
        public LibraryDbContext()
        {
            _entitySets = new Dictionary<Type, IList>
            {
                [typeof(Author)] = Authors,
                [typeof(Book)] = Books
            };
        }

        public List<Author> Authors { get; } = new();
        public List<Book> Books { get; } = new();

        private readonly Dictionary<Type, IList> _entitySets;

        public List<T> GetEntitySet<T>() where T : class
        {
            if (_entitySets.TryGetValue(typeof(T), out var entitySet))
            {
                return (List<T>)entitySet;
            }
            throw new InvalidOperationException($"No entity set found for {typeof(T).Name}");
        }
    }
}

  
