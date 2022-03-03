namespace PizzaStore.Infra;

public interface IRepository<T>
{
    Task<RepositoryResponse> Add(T entity);
    Task<RepositoryResponse> Remove(int id);
    Task<RepositoryResponse> Update(T entity);
    Task<T?> Get(int id);
    Task<IEnumerable<T>> Get(int skip, int take);
}