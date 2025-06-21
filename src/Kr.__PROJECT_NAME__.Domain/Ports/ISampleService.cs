using System;
using Kr.__PROJECT_NAME__.Domain.Dto;

namespace Kr.__PROJECT_NAME__.Domain.Ports;

public interface ISampleService
{
    Task<SampleDto> Get(string id);
}
