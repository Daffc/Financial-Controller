namespace FinancialControllerServer.Domain.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException(string message, List<string>? errors = null) : base(message, StatusCodes.Status404NotFound, errors) { }
}