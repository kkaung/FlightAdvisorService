using System;

class Haversine
{
    const double R = 6371; //Radius of the Earth in km

    public static double Distance(double lat1, double lon1, double lat2, double lon2)
    {
        double dLat = ToRadians(lat2 - lat1);
        double dLon = ToRadians(lon2 - lon1);
        lat1 = ToRadians(lat1);
        lat2 = ToRadians(lat2);

        double a =
            Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
            + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return Math.Round(R * c, 2);
    }

    private static double ToRadians(double angle)
    {
        return Math.PI * angle / 180.0;
    }
}
