
namespace MeetPoint.Api.DTOs;

public class CandidateVenueResponse
{
    public string VenueId { get; set; } = string.Empty;

    public string VenueName { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public double Rating { get; set; }

    public string PriceLevel { get; set; } = string.Empty;

    public double DistanceFromMidpointKm { get; set; }

    public List<ParticipantVenueDistanceResponse>
        ParticipantDistances { get; set; } = new();

    public double TotalDistanceKm { get; set; }
    public double MaximumParticipantDistanceKm { get; set; }
    public double AverageParticipantDistanceKm { get; set; }

    public int Rank { get; set; }
}

public class ParticipantVenueDistanceResponse
{
    public string ParticipantId { get; set; } = string.Empty;

    public double DistanceKm { get; set; }
}
