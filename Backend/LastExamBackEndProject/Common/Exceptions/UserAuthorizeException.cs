namespace LastExamBackEndProject.Common.Exceptions;

public class UserAuthorizeException : BaseBusinessException
{
    public UserAuthorizeException(string message) : base(ExceptionCode.UserAuthorizeException, message)
    {
    }

    public UserAuthorizeException(string message, Exception innerException) : base(ExceptionCode.UserAuthorizeException, message, innerException)
    {
    }
}