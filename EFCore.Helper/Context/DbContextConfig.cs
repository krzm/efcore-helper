using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace EFCore.Helper;

public abstract class DbContextConfig
    : DbContextBase
{
    private static readonly LoggerFactory debugLoggerFactory = 
        new LoggerFactory(new[] { new DebugLoggerProvider() });

    protected override void SetConfig(DbContextOptionsBuilder builder)
    {
        var helper = new DbConfigHelper();

        builder.UseSqlServer(
            helper.GetConnectionString());
        
        if (helper.Config.UseLogger)
            builder.UseLoggerFactory(debugLoggerFactory);
    }
}