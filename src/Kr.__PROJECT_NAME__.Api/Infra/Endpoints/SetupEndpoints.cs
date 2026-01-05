
namespace Kr.__PROJECT_NAME__.Api;

public static partial class ApiEndpoints
{
    public static void RegisterEndpoints(this IEndpointRouteBuilder app)
    {
        var versionBuilder = app.NewVersionedApi();
        SampleEndpoints(app, versionBuilder);
    }
}