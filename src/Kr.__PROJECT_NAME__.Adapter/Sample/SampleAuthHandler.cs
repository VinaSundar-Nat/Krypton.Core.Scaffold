using Kr.__PROJECT_NAME__.Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Kr.__PROJECT_NAME__.Adapter.Sample;

public class SampleAuthHandler(IOptions<ServiceConfiguration> Options, IHttpContextAccessor HttpContextAccessor) : DelegatingHandler
{
    private readonly ServiceConfiguration _options = Options.Value;
    private readonly IHttpContextAccessor _httpContextAccessor = HttpContextAccessor;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Example of using the options and http context
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        // Add custom headers or modify the request as needed
        request.Headers.Add("X-User-Id", userId);

        return await base.SendAsync(request, cancellationToken);
    }

}