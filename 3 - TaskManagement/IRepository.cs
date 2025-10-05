namespace TaskManagement
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        bool Add(T item);
        bool Update(T item);
        bool Delete(int id);
    }
}
