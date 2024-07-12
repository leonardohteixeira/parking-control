namespace Parking.Domain.Abstractions.Models
{
    public class ParkingValues
    {
        public Guid ParkingValuesId { get; set; }
        public decimal? Value { get; set; }

        // Minutes that will count as +1 hour or not on calculate
        public int? HourlyTolerance { get; set; }

        public bool HalfInTheFirstHour { get; set; }
    }
}
