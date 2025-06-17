using RentEase.Domain.Common;

namespace RentEase.Domain;

public class Service : BaseDomainModel
{
    public DateTime Date { get; set; }
    public int CarId { get; set; }
    public virtual Car? Car { get; set; }
}