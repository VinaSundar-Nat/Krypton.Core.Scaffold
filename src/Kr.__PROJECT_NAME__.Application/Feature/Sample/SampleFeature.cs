using System;
using Kr.__PROJECT_NAME__.Domain.Dto;
using Kr.__PROJECT_NAME__.Domain.Ports;

namespace Kr.__PROJECT_NAME__.Application.Feature.Sample;

public class SampleFeature(ISampleService SampleService) : ISampleFeature
{
    public async Task<SampleDto> Samples(string id)
    {
        return await SampleService.Get(id);
    }
}
