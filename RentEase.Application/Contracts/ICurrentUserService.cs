namespace RentEase.Application.Contracts
{
    public interface ICurrentUserService
    {
        string UserName { get; }
        string Email { get; }

        string FullName { get; }
        string Address { get; }
    }
}
