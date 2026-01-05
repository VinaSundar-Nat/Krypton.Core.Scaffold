using Kr.__PROJECT_NAME__.Api.Infra.Helpers;
using Kr.__PROJECT_NAME__.Domain.Dto;
using Kr.__PROJECT_NAME__.Domain.Ports;
using Kr.Common.Infrastructure.Http.Models;


namespace Kr.__PROJECT_NAME__.Api;

public static partial class ApiEndpoints
{
    public static void SampleEndpoints(IEndpointRouteBuilder app, IVersionedEndpointRouteBuilder coreAppBuilder)
    {
        var sampleGroup = coreAppBuilder.MapGroup("/api/doc/sample/v1").HasApiVersion( 1.0 );
        sampleGroup.MapGet("/", async ([AsParameters] ApiHeaders request,
                HttpContext context,
                [FromServices] ISampleFeature sampleFeature,
                CancellationToken token = default) =>
        {
            var sample = await sampleFeature.Samples("TEST");
            return Results.Ok(new ApiSuccessResponse<SampleDto> { StatusCode = 200, Url = context.Request.Path ,Data = sample });
        }).WithOpenApi(operation =>
            operation.GenerateOpenApiDoc(
                "v1 sample get.",
                "sample get endpoint to test the api.",
                "Sample",
                "File opertaions to support enterprise operations."
        ))
        .Produces<ApiSuccessResponse<SampleDto>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
