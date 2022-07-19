using MyConfigTool = Config.Wrapper.ConfigTool;

namespace EFCore.Helper;

public class DbConfigHelper
{
    private DbConfig config;

    public DbConfig Config => config;

    public DbConfigHelper()
    {
        config = MyConfigTool.ReadConfig<DbConfig>()!;
        ArgumentNullException.ThrowIfNull(config);
    }

    public string GetConnectionString()
    {
        switch (config.DbType)
        {
            case DbType.Local:
                return config.LocalConnectionString!;
            case DbType.LocalTest:
                return config.LocalTestConnectionString!;
            default :
                return config.LocalConnectionString!;
        }
    }
}