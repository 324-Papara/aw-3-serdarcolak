using System.Linq.Expressions;
using Para.Base.Response;
using Para.Schema;

namespace Para.Data.GenericRepository;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetById(long Id);
    Task Insert(TEntity entity);
    
    //void Update(TEntity entity);
    
    Task Update(long Id, TEntity entity);
    void Delete(TEntity entity);
    Task Delete(long Id);
    Task<List<TEntity>> GetAll();
    
    Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);
    
    Task<List<TEntity>> Include(params Expression<Func<TEntity, object>>[] includes);
    
}