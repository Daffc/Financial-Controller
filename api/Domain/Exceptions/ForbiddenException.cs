namespace FinancialControllerServer.Domain.Exceptions;

public class ForbiddenException : AppException
{
    public ForbiddenException(string message, List<string>? errors = null) : base(message, StatusCodes.Status403Forbidden, errors) { }
}