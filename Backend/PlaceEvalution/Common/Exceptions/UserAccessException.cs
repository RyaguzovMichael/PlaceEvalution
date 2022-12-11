namespace PlaceEvolution.API.Common.Exceptions;

public class UserAccessException : BaseBusinessException
{
    public UserAccessException(string message) : base(ExceptionCode.UserAccessException, message)
    {
    }

    public UserAccessException(string message, Exception innerException) : base(ExceptionCode.UserAccessException, message, innerException)
    {
    }
}