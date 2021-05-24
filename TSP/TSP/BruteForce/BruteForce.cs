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
            var routes = _routeHelper.GenerateAllPossibleRoutes(coordinates);
        }
    }
}