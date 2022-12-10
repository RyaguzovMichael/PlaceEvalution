namespace LastExamBackEndProject.Common.Exceptions;

public class DbException : BaseBusinessException
{
    public DbException(string message) : base(ExceptionCode.DbException, message)
    {
    }

    public DbException(string message, Exception innerException) : base(ExceptionCode.DbException, message, innerException)
    {
    }
}