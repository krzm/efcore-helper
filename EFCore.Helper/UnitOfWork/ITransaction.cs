using Microsoft.EntityFrameworkCore.Storage;

namespace EFCore.Helper;

public interface ITransaction
{
    IDbContextTransaction BeginTransaction();

    Task<IDbContextTransaction> BeginTransactionAsync(
        CancellationToken cancellationToken = default);
}