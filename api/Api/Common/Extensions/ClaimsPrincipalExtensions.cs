using System.Security.Claims;
using FinancialControllerServer.Domain.Exceptions;

namespace FinancialControllerServer.Api.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst(ClaimTypes.NameIdentifier);

        if(claim is null)
            throw new UnauthorizedException("Token inválido");

        if(!Guid.TryParse(claim.Value, out var userId))
            throw new UnauthorizedException("Id de usuário de token inválido");
    
        return userId;
    }
}