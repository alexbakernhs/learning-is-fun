namespace TSP
{
    using System.Collections.Generic;
    public interface IRouteHelper
    {
        List<List<Coordinate>> GenerateAllPossibleRoutes(List<Coordinate> coords);
    }
}