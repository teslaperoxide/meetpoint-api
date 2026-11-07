
using MeetPoint.Api.Models;

namespace MeetPoint.Api.Services;

public static class GeoDistanceCalculator
{
    private const double EarthRadiusKm = 6371;

    public static double CalculateDistanceKm(
        GeoLocation point1,
        GeoLocation point2)
    {
        var latitudeDifference = ToRadians(
            point2.Latitude - point1.Latitude);

        var longitudeDifference = ToRadians(
            point2.Longitude - point1.Longitude);

        var latitude1 = ToRadians(point1.Latitude);
        var latitude2 = ToRadians(point2.Latitude);

        var a =
            Math.Pow(Math.Sin(latitudeDifference / 2), 2) +
            Math.Cos(latitude1) *
            Math.Cos(latitude2) *
            Math.Pow(Math.Sin(longitudeDifference / 2), 2);

        var c = 2 * Math.Asin(Math.Sqrt(Math.Min(1, a)));

        return EarthRadiusKm * c;
    }

    private static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
}
