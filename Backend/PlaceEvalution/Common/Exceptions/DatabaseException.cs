namespace PlaceEvalution.API.Common.Exceptions;

public class DatabaseException : BaseBusinessException
{
    public DatabaseException(string message) : base(ExceptionCode.DbException, message)
    {
    }

    public DatabaseException(string message, Exception innerException) : base(ExceptionCode.DbException, message, innerException)
    {
    }
}