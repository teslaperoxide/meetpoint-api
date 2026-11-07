
using MeetPoint.Api.Models;

namespace MeetPoint.Api.DTOs;

public class MidpointResponse
{
    public GeoLocation Midpoint { get; set; } = new();

    public int ParticipantCount { get; set; }
}
