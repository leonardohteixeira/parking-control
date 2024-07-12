namespace Parking.Server.Abstractions.Models;

public class Parkings
{
    public Guid? ParkingsId { get; set; }
    public string? Plate { get; set; }
    public DateTime? ArrivalTime { get; set; }
    public DateTime? DepartureTime { get; set; }
    public decimal? TotalValue { get; set; }
}

