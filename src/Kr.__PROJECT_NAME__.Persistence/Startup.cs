using System;
using Kr.__PROJECT_NAME__.Common.Infrastructure.Datastore;
using Kr.__PROJECT_NAME__.Common.Infrastructure.Datastore.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kr.__PROJECT_NAME__.Persistence;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
         services.Configure<DbSettings>(configuration.GetSection("DataStore:Sample"));
         services.DbNpgContextPoolSettings<SampleDbContext>(configuration, Constants.SampleDataKey);
    }
}
