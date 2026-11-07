
using MeetPoint.Api.DTOs;
using MeetPoint.Api.Models;

namespace MeetPoint.Api.Interfaces;

public interface ILocationService
{
    GeoLocation CalculateMidpoint(
        List<ParticipantLocationRequest> participants);
}
