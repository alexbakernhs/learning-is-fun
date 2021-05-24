using System.Collections.Generic;

namespace TSP
{
    public interface ICoordinateHelper
    {
        IEnumerable<Coordinate> GenerateCoords(bool random = false, int size = 0);
        double TotalRouteDistance();
        double DistanceBetweenPoints(Coordinate one, Coordinate two);
    }
}