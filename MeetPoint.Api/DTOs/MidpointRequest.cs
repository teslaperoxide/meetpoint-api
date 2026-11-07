
using System.ComponentModel.DataAnnotations;

namespace MeetPoint.Api.DTOs;

public class MidpointRequest
{
    [Required]
    [MinLength(2, ErrorMessage = "At least two participants are required.")]
    public List<ParticipantLocationRequest> Participants { get; set; } = new();
}

public class ParticipantLocationRequest
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
    public double Latitude { get; set; }

    [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
    public double Longitude { get; set; }
}
