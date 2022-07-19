using Microsoft.EntityFrameworkCore;

namespace EFCore.Helper;

public abstract class DbContextBase
    : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        SetConfig(builder);
    }

    protected abstract void SetConfig(DbContextOptionsBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SetPolicy(builder);
        SetShadowProps(builder);
        SeedData(builder);
    }

    protected abstract void SetPolicy(ModelBuilder builder);

    protected abstract void SetShadowProps(ModelBuilder builder);

    protected abstract void SeedData(ModelBuilder builder);
}