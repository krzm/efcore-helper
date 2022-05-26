using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCore.Helper;

public abstract class UnitOfWork 
	: IUnitOfWork
{
	private readonly DbContext context;
	private bool disposed = false;

    public UnitOfWork(DbContext context)
    {
        this.context = context;
    }

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!disposed)
		{
			if (disposing)
			{
				context.Dispose();
			}
		}
		disposed = true;
	}

    public void Save() => 
		context.SaveChanges();

    public IDbContextTransaction BeginTransaction() => 
        context.Database.BeginTransaction();

    public Task<IDbContextTransaction> BeginTransactionAsync(
        CancellationToken cancellationToken = default) => 
            context.Database.BeginTransactionAsync(cancellationToken);
}