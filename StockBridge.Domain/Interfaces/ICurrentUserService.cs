namespace StockBridge.Domain.Interfaces;

public interface ICurrentUserService
{
    int? UserId { get; }
    bool IsAuthenticated { get; }
}