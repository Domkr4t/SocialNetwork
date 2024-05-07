namespace SocialNetwork.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task Create(T entity);
        Task Delete(T entity);
        IQueryable<T> GetAll();
        Task<T> Update(T entity);
        Task Attach(T entity);
    }
}
