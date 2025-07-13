using Kr.__PROJECT_NAME__.Common.Extensions;
using Kr.__PROJECT_NAME__.Common.Infrastructure.Datastore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Kr.__PROJECT_NAME__.Common.Infrastructure.Datastore;

public abstract class BaseContext<T>(DbContextOptions<T> options,
                    IOptions<DbSettings> dbSettings) : DbContext(options) where T : DbContext
{
    private readonly DbSettings? _dbSettings = dbSettings?.Value;
  
  
    public async override Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        //TODO: Implement any pre-save logic here, such as auditing or event notification.
        await NotifyChanges();
        return await base.SaveChangesAsync(token);
    }

    public abstract Task NotifyChanges();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_dbSettings?.IsValid ?? false && !optionsBuilder.IsConfigured)
        {
            var dbType = _dbSettings.DbType.ToEnum<DbType>();

            switch (dbType)
            {
                case DbType.SqlServer:
                    optionsBuilder.UseSqlServer(_dbSettings.ConnectionString);
                    break;
                case DbType.PostgreSQL:
                    optionsBuilder.UseNpgsql(_dbSettings.ConnectionString);
                    break;
                default:
                    throw new NotSupportedException($"Database type '{dbType}' is not supported.");
            }
        }
    }
}
