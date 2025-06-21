namespace Kr.__PROJECT_NAME__.Common.Exceptions;

public class MessageException : Exception
{
    public MessageException(Exception ex, string message)
        : base(message, ex)
    {
	}
}

