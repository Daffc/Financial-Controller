using FinancialControllerServer.Domain.Entities;

namespace FinancialControllerServer.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateToken(Usuario usuario);
}