namespace FinancialControllerServer.Domain.Exceptions;

public sealed class BadRequestException : AppException
{
    public BadRequestException(string message, List<string>? errors = null) : base(message, StatusCodes.Status400BadRequest, errors) { }
}