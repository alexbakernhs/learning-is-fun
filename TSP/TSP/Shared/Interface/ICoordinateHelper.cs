using System.Collections.Generic;

namespace TSP
{
    public interface ICoordinateHelper
    {
        IEnumerable<Coordinate> GenerateCoords(bool random = false, int size = 0, int minX = 0, int maxX = 0, int minY = 0, int maxY = 0);
        double DistanceBetweenPoints(Coordinate one, Coordinate two);
    }
}