using Library.DataAccess.Data;

namespace Library.DataAccess.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : class, new()
    {
        protected readonly List<T> _entityList;
        private int _nextId = 1;

        public InMemoryRepository(LibraryListContext context)
        {
            _entityList = context.GetEntitySet<T>();
        }

        protected virtual int GetId(T entity)
        {
            var prop = typeof(T).GetProperty("Id");
            var result = (int)prop.GetValue(entity);
            return result;
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
        public Task<T> AddAsync(T entity)
        {
            SetId(entity, _nextId++);
            _entityList.Add(entity);

            var result = Task.FromResult(entity);
            return result;
        }
        public Task<T?> UpdateAsync(T entity)
        {
            var id = GetId(entity);
            var existingEntity = _entityList.FirstOrDefault(e => GetId(e) == id);

            if (existingEntity != null)
            {
                UpdateExistingEntity(existingEntity, entity);
                return Task.FromResult<T?>(existingEntity);
            }

            var result = Task.FromResult<T?>(null);
            return result;
        }

        public Task<bool> DeleteAsync(int id)
        {
            var entity = _entityList.FirstOrDefault(e => GetId(e) == id);
            
            if (entity != null)
            {
                _entityList.Remove(entity);
                return Task.FromResult(true);
            }

            var result = Task.FromResult(false);
            return result;
        }

        public Task<List<T>> GetAllAsync()
        {
            var result = Task.FromResult(_entityList);
            return result;
        }

        public Task<T?> GetByIdAsync(int id)
        {
            var entity = _entityList.FirstOrDefault(e => GetId(e) == id);
            var result = Task.FromResult(entity);
            return result;
        }

    }
}
