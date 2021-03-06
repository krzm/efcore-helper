using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EFCore.Helper;

public class EFRepository<TEntity, TContext>
	: IRepository<TEntity>
		where TEntity : class
		where TContext : DbContext
{
	private readonly TContext context;
	private readonly DbSet<TEntity> dbSet;

	public EFRepository(TContext context)
	{
		this.context = context;
		dbSet = context.Set<TEntity>();
	}

	public virtual IEnumerable<TEntity> Get(
		Expression<Func<TEntity, bool>>? filter = null
		, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null
		, string includeProperties = "")
	{
		IQueryable<TEntity> query = dbSet;

		if (filter != null)
		{
			query = query.Where(filter);
		}

		foreach (var includeProperty in includeProperties.Split
			(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
		{
			query = query.Include(includeProperty);
		}

		if (orderBy != null)
		{
			return orderBy(query).ToList();
		}
		else
		{
			return query.ToList();
		}
	}

	public virtual TEntity GetByID(object id)
	{
		TEntity? entity = dbSet.Find(id);
		ArgumentNullException.ThrowIfNull(entity);
		return entity;
	}

	public virtual void Insert(TEntity entity)
	{
		dbSet.Add(entity);
	}

	public virtual void Update(TEntity entityToUpdate)
	{
		dbSet.Attach(entityToUpdate);
		context.Entry(entityToUpdate).State = EntityState.Modified;
	}

	public virtual void Delete(object id)
	{
		TEntity? entityToDelete = dbSet.Find(id);
		ArgumentNullException.ThrowIfNull(entityToDelete);
		Delete(entityToDelete);
	}

	public virtual void Delete(TEntity entityToDelete)
	{
		if (context.Entry(entityToDelete).State == EntityState.Detached)
		{
			dbSet.Attach(entityToDelete);
		}
		dbSet.Remove(entityToDelete);
	}
}