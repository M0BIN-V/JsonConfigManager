namespace JsonConfigManager;

internal class InvalidJsonConfigFileException : Exception
{
    public InvalidJsonConfigFileException() : base()
    {
    }

    public InvalidJsonConfigFileException(string? message) : base(message)
    {
    }

    public InvalidJsonConfigFileException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
