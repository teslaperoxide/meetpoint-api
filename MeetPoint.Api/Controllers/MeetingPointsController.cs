
using Microsoft.AspNetCore.Mvc;
using MeetPoint.Api.DTOs;
using MeetPoint.Api.Interfaces;
using MeetPoint.Api.Models;
using MeetPoint.Api.Services;

namespace MeetPoint.Api.Controllers;

[ApiController]
[Route("api/v1/meeting-points")]
public class MeetingPointsController : ControllerBase
{
    private readonly ILocationService _locationService;
    private readonly IVenueProvider _venueProvider;

    public MeetingPointsController(
        ILocationService locationService,
        IVenueProvider venueProvider)
    {
        _locationService = locationService;
        _venueProvider = venueProvider;
    }

    [HttpPost("midpoint")]
    public ActionResult<MidpointResponse> CalculateMidpoint(
        [FromBody] MidpointRequest request)
    {
        var midpoint = _locationService.CalculateMidpoint(
            request.Participants);

        var response = new MidpointResponse
        {
            Midpoint = midpoint,
            ParticipantCount = request.Participants.Count
        };

        return Ok(response);
    }

    [HttpPost("candidates")]
    public async Task<ActionResult<
        IReadOnlyList<CandidateVenueResponse>>> GetCandidateVenues(
        [FromBody] MidpointRequest request,
        CancellationToken cancellationToken)
    {
        var midpoint = _locationService.CalculateMidpoint(
            request.Participants);

        var venues = await _venueProvider.GetCandidateVenuesAsync(
            midpoint,
            cancellationToken);

var response = venues.Select(venue =>
{
    var participantDistances = request.Participants
        .Select(participant =>
        {
            var participantLocation = new GeoLocation
            {
                Latitude = participant.Latitude,
                Longitude = participant.Longitude
            };

            var venueLocation = new GeoLocation
            {
                Latitude = venue.Latitude,
                Longitude = venue.Longitude
            };

            var distance = GeoDistanceCalculator.CalculateDistanceKm(
                participantLocation,
                venueLocation);

            return new ParticipantVenueDistanceResponse
            {
                ParticipantId = participant.Id,
                DistanceKm = Math.Round(distance, 2)
            };
        })
        .ToList();

            return new CandidateVenueResponse
            {
                VenueId = venue.Id,
                VenueName = venue.Name,
                Category = venue.Category,
                Latitude = venue.Latitude,
                Longitude = venue.Longitude,
                Rating = venue.Rating,
                PriceLevel = venue.PriceLevel,
                DistanceFromMidpointKm = venue.DistanceFromMidpointKm,

                ParticipantDistances = participantDistances,

                TotalDistanceKm = Math.Round(
                    participantDistances.Sum(x => x.DistanceKm), 2),

                MaximumParticipantDistanceKm = participantDistances.Max(
                    x => x.DistanceKm),

                AverageParticipantDistanceKm = Math.Round(
                    participantDistances.Average(x => x.DistanceKm), 2)
            };
        })
        .OrderBy(venue => venue.MaximumParticipantDistanceKm)
        .ThenBy(venue => venue.TotalDistanceKm)
        .Select((venue, index) =>
        {
            venue.Rank = index + 1;
            return venue;
        })
        .ToList();

        return Ok(response);
    }
}
