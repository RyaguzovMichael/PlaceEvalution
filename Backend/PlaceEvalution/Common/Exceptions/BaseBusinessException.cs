namespace PlaceEvolution.API.Common.Exceptions;

public class BaseBusinessException : ApplicationException
{
    public ExceptionCode ExceptionCode { get; }

    public BaseBusinessException(ExceptionCode code, string message) : base(message)
    {
        ExceptionCode = code;
    }

    public BaseBusinessException(ExceptionCode code, string message, Exception innerException) : base(message, innerException)
    {
        ExceptionCode = code;
    }
}