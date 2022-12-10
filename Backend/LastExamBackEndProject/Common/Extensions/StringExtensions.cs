using LastExamBackEndProject.Common.Exceptions;

namespace LastExamBackEndProject.Common.Extensions;

public static class StringExtensions
{
    public static void VerifyStringLength(this string str, int maxLength, int minLength = 0)
    {
        if (str.Length < minLength)
        {
            throw new ValidationDataException($"String length can't be less then {minLength} characters.");
        }

        if (str.Length > maxLength)
        {
            throw new ValidationDataException($"String length can't be longer then {maxLength} characters.");
        }
    }
}