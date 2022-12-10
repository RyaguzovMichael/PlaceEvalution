using LastExamBackEndProject.Common.Exceptions;

namespace LastExamBackEndProject.API.Models;

public class DefaultResponse<TData>
{
    public TData? Value { get; }
    public bool IsSuccess { get; }
    public ExceptionCode? ExceptionCode { get; }
    public string? Exception { get; }

    public DefaultResponse(TData data)
    {
        Value = data;
        IsSuccess = true;
        ExceptionCode = null;
        Exception = null;
    }

    public DefaultResponse(ExceptionCode code, string exceptionMessage)
    {
        Value = default;
        IsSuccess = false;
        ExceptionCode = code;
        Exception = exceptionMessage;
    }
}