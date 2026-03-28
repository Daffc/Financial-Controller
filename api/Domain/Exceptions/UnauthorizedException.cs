namespace FinancialControllerServer.Domain.Exceptions;

public class UnauthorizedException : AppException
{
    public UnauthorizedException(string message, List<string>? errors = null) : base(message, StatusCodes.Status401Unauthorized, errors) { }
}