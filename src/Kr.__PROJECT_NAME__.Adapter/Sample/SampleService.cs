using System;
using Kr.__PROJECT_NAME__.Domain.Common;
using Kr.__PROJECT_NAME__.Domain.Dto;
using Kr.__PROJECT_NAME__.Domain.Ports;
using Microsoft.Extensions.Options;

namespace Kr.__PROJECT_NAME__.Adapter.Sample;

public class SampleService(IOptions<ServiceConfiguration> ServiceOptions) : ISampleService
{
    private readonly ServiceConfiguration _serviceConfiguration = ServiceOptions.Value;
    
    public async Task<SampleDto> Get(string id)
    {
        throw new NotImplementedException();
    }
}
