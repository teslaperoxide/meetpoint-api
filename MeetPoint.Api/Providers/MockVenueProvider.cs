
using MeetPoint.Api.Interfaces;
using MeetPoint.Api.Models;
using MeetPoint.Api.Services;

namespace MeetPoint.Api.Providers;

public class MockVenueProvider : IVenueProvider
{
    private const double SearchRadiusKm = 5;

    public Task<IReadOnlyList<Venue>> GetCandidateVenuesAsync(
        GeoLocation midpoint,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        IReadOnlyList<Venue> allVenues = new List<Venue>
        {
            new Venue
            {
                Id = "V001",
                Name = "Sample Cafe A",
                Category = "Cafe",
                Latitude = 18.5300,
                Longitude = 73.8500,
                Rating = 4.3,
                PriceLevel = "Moderate"
            },
            new Venue
            {
                Id = "V002",
                Name = "Sample Restaurant B",
                Category = "Restaurant",
                Latitude = 18.5350,
                Longitude = 73.8650,
                Rating = 4.5,
                PriceLevel = "Expensive"
            },
            new Venue
            {
                Id = "V003",
                Name = "Sample Coffee Shop C",
                Category = "Cafe",
                Latitude = 18.5250,
                Longitude = 73.8600,
                Rating = 4.1,
                PriceLevel = "Budget"
            },
            new Venue
            {
                Id = "V004",
                Name = "Sample Cafe D",
                Category = "Cafe",
                Latitude = 18.6000,
                Longitude = 73.9000,
                Rating = 4.0,
                PriceLevel = "Budget"
            }
        };

        var nearbyVenues = new List<Venue>();

        foreach (var venue in allVenues)
        {
            var distance = GeoDistanceCalculator.CalculateDistanceKm(
                                                        midpoint,
                                                        new GeoLocation
                                                        {
                                                            Latitude = venue.Latitude,
                                                            Longitude = venue.Longitude
                                                        });

            if (distance <= SearchRadiusKm)
            {
                venue.DistanceFromMidpointKm = Math.Round(distance, 2);

                nearbyVenues.Add(venue);
            }
        }

        return Task.FromResult<IReadOnlyList<Venue>>(
            nearbyVenues);
    }
}
