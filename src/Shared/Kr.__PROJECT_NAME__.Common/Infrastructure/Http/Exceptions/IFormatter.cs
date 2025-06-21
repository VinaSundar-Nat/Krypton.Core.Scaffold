using System;
namespace Kr.__PROJECT_NAME__.Common.Infrastructure.Http;

public interface IFormatter<ErrorHandler>
{
    void Verify(HttpResponseMessage response);
}


