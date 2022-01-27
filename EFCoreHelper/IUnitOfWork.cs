namespace EFCoreHelper;

public interface IUnitOfWork : IDisposable
{
    void Save();
}