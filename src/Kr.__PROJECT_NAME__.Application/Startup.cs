using Kr.__PROJECT_NAME__.Infrastructure;
using Kr.__PROJECT_NAME__.Application.Feature.Sample;
using Kr.__PROJECT_NAME__.Domain.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kr.__PROJECT_NAME__.Application;

public static class Startup
{
     public static void RegisterFeatures(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterServices(configuration);
        services.AddScoped<ISampleFeature, SampleFeature>();
    }

}
