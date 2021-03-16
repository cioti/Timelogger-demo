namespace Timelogger.Domain.Abstractions
{
    public interface ICurrentUserService
    {
        string Username { get; }
        string UserId { get; }
    }
}
