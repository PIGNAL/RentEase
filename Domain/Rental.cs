using RentEase.Domain.Common;

namespace RentEase.Domain
{
    public class Rental: BaseDomainModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public int CarId { get; set; }
        public virtual Car? Car { get; set; }

    }
}
