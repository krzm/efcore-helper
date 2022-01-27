using System.Linq.Expressions;

namespace EFCoreHelper;

public interface IGenericRepository<TEntity> 
    where TEntity : class
{
    IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null
        , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null
        , string includeProperties = "");

    TEntity GetByID(object id);

    void Insert(TEntity entity);

    void Update(TEntity entityToUpdate);

    void Delete(object id);

    void Delete(TEntity entityToDelete);
}