namespace Parking.Server.Abstractions.Models
{
    public class Parkings
    {
        public Guid? ParkingsId { get; set; }
        public string? Plate { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public DateTime? DepartureTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? BilledTime { get; set; }
        public decimal? Price { get; set; }
        public decimal? AmountDue { get; set; }
        public DateTime? Date { get; set; }
    }
}
