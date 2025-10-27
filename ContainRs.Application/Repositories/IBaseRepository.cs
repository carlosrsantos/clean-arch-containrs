namespace ContainRs.Application.Repositories;


public interface IBaseRepository<T> where T : class
{
    T GetById(Guid id); 
    Task<IEnumerable<T>> GetAsync();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}
