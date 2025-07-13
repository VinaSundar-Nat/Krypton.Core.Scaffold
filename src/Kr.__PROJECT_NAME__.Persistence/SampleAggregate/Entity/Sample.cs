using System;
using Kr.__PROJECT_NAME__.Common.Infrastructure.Datastore;
using Kr.__PROJECT_NAME__.Persistence.SampleAggregate.ValueObject;

namespace Kr.__PROJECT_NAME__.Persistence.SampleAggregate.Entity;

public sealed class Sample : BaseEntity<Sample>
{
    public string? Name { get; set; }
    public Price? Price { get; set; }    
}
