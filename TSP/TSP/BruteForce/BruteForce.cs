namespace TSP
{
    using System;
    using System.Collections.Generic;
    public class BruteForce
    {
        private readonly IRouteHelper _routeHelper;
        public BruteForce(IRouteHelper routeHelper)
        {
            _routeHelper = routeHelper;
        }
        public void Run(List<Coordinate> coordinates)
        {
            double? bestDistance = null;
            List<Coordinate> bestRoute = null;
            var routes = _routeHelper.GenerateAllPossibleRoutes(coordinates);
            foreach(var route in routes)
            {
                var routeDistance = _routeHelper.TotalRouteDistance(route);
                if(bestDistance == null || routeDistance < bestDistance)
                {
                    bestDistance = routeDistance;
                    bestRoute = route;
                }
            }
            foreach(var coord in bestRoute)
            {
                System.Console.Write(coord.PrintCoordinate());
            }
            System.Console.WriteLine(bestDistance);
        }
    }
}