namespace TSP
{
    using System.Collections.Generic;
    public interface IRouteHelper
    {
        List<List<Coordinate>> GenerateAllPossibleRoutes(List<Coordinate> coords);
        double TotalRouteDistance(List<Coordinate> coords);
        List<Coordinate> GenerateRandomRoute(List<Coordinate> coords);
    }
}