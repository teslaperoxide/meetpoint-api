
using MeetPoint.Api.Models;

namespace MeetPoint.Api.Interfaces;

public interface IVenueProvider
{
    Task<IReadOnlyList<Venue>> GetCandidateVenuesAsync(
        GeoLocation midpoint,
        CancellationToken cancellationToken = default);
}
