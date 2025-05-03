namespace Film_Rating_System.Models.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        int Add(T item);
        int Edit(int id, T item);
        int Delete(int id);
    }
}
