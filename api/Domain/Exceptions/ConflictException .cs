namespace FinancialControllerServer.Domain.Exceptions;

public class ConflictException  : AppException
{
    public ConflictException (string message, List<string>? errors = null) : base(message, StatusCodes.Status409Conflict, errors) { }
}