using RentEase.Domain.Common;

namespace RentEase.Domain
{
    public class Customer : BaseDomainModel
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
    }
}
