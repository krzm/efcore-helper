namespace EFCore.Helper;

public interface IUnitOfWork 
    : IDisposable
        , ITransaction
{
    void Save();
}