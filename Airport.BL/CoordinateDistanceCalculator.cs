using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.BL
{
    public class CoordinateDistanceCalculator
    {
        public static double Distance(Coordinates fromCoordinates,Coordinates toCoordinates)
        {
            var baseRad = Math.PI * fromCoordinates.Latitude / 180;
            var targetRad = Math.PI * toCoordinates.Latitude / 180;
            var theta = fromCoordinates.Longitude - toCoordinates.Longitude;
            var thetaRad = Math.PI * theta / 180;

            double dist =
                Math.Sin(baseRad) * Math.Sin(targetRad) + Math.Cos(baseRad) *
                Math.Cos(targetRad) * Math.Cos(thetaRad);
            dist = Math.Acos(dist);

            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            return dist * 1.609344;
        }
    }
    public class Coordinates
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public Coordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }   
}
