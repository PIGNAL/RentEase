using RentEase.Domain.Common;

namespace RentEase.Domain
{
    public class Car : BaseDomainModel
    {
        public string? Type { get; set; }
        public string? Model { get; set; }
        public virtual ICollection<Service>? Services { get; set; }
    }
}
