
using MeetPoint.Api.DTOs;
using MeetPoint.Api.Interfaces;
using MeetPoint.Api.Models;

namespace MeetPoint.Api.Services;

public class LocationService : ILocationService
{
    public GeoLocation CalculateMidpoint(
        List<ParticipantLocationRequest> participants)
    {
        var averageLatitude = participants.Average(
            participant => participant.Latitude);

        var averageLongitude = participants.Average(
            participant => participant.Longitude);

        return new GeoLocation
        {
            Latitude = averageLatitude,
            Longitude = averageLongitude
        };
    }
}
