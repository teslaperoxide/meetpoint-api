
namespace MeetPoint.Api.Models;

public class Venue
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public double Rating { get; set; }

    public string PriceLevel { get; set; } = string.Empty;

    public double DistanceFromMidpointKm { get; set; }
}
