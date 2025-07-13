using Kr.__PROJECT_NAME__.Common.Infrastructure.Datastore;
using Kr.__PROJECT_NAME__.Common.Infrastructure.Datastore.Model;
using Kr.__PROJECT_NAME__.Persistence.Configuration;
using Kr.__PROJECT_NAME__.Persistence.SampleAggregate.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Kr.__PROJECT_NAME__.Persistence;

public class SampleDbContext(DbContextOptions<SampleDbContext> options,
    IOptions<DbSettings> dbSettings) : BaseContext<SampleDbContext>(options, dbSettings)
{
      public DbSet<Sample> Samples { get; set; }

    public override Task NotifyChanges()
    {
        // Implement your change notification logic here
        return Task.CompletedTask;
    }
    
     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("kr");
 
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasSequence<int>("sampleseq", "kr")
          .StartsAt(1)
          .HasMax(30000)
          .IsCyclic()
          .IncrementsBy(1);

        modelBuilder.ApplyAllConfigurations(typeof(SampleConfiguration));
    }
}
