namespace Kr.__PROJECT_NAME__.Api.Infra;

public  static partial class LoggerExtensions
{
    [LoggerMessage(EventId =1, EventName ="ApplicationException",Level = LogLevel.Error, Message ="Server HTTPS configuration exception")]
    public static partial void LogApplicationException(this ILogger logger, Exception exception);
}
