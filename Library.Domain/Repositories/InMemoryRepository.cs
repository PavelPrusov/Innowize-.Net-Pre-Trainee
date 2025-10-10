
namespace Library.DataAccess.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : class, new()
    {
        protected readonly List<T> _entities = new();
        private int _nextId = 1;
        protected virtual int GetId(T entity)
        {
            var prop = typeof(T).GetProperty("Id");
            return (int)prop.GetValue(entity);
        }
        protected virtual void SetId(T entity, int id)
        {
            var prop = typeof(T).GetProperty("Id");
            prop.SetValue(entity, id);
        }
       protected virtual void UpdateExistingEntity(T existingEntity, T newEntity)
        {
            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                if (prop.Name != "Id" && prop.CanWrite)
                {
                    var value = prop.GetValue(newEntity);
                    prop.SetValue(existingEntity, value);
                }
            }
        }
        public Task<T> CreateAsync(T entity)
        {
            SetId(entity, _nextId++);
            _entities.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var entity = _entities.FirstOrDefault(e => GetId(e) == id);
            if (entity != null)
            {
                _entities.Remove(entity);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<List<T>> GetAllAsync()
        {
            return Task.FromResult(_entities.ToList());
        }

        public Task<T?> GetByIdAsync(int id)
        {
            var entity = _entities.FirstOrDefault(e => GetId(e) == id);
            return Task.FromResult(entity);
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }

        public Task<T?> UpdateAsync(T entity)
        {
            var id = GetId(entity);
            var existingEntity = _entities.FirstOrDefault(e => GetId(e) == id);

            if (existingEntity != null)
            {
                UpdateExistingEntity(existingEntity, entity);
                return Task.FromResult<T?>(existingEntity);
            }

            return Task.FromResult<T?>(null);
        }
    }
}
