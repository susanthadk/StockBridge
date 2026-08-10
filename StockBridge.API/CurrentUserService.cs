using StockBridge.Domain.Interfaces;
using System.Security.Claims;

namespace StockBridge.API;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public int? UserId
    {
        get
        {
            var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Try to parse the string claim into an int
            if (int.TryParse(userIdClaim, out var parsedInt))
            {
                return parsedInt;
            }

            return null;
        }
    }
    public bool IsAuthenticated => httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}