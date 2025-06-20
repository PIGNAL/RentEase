namespace RentEase.Application.Models
{
    public class RentalDto
    {
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CarDto Car { get; set; }
    }
}
