namespace TSP
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Logging;

    public class BruteForce
    {
        private readonly IRouteHelper _routeHelper;
        private readonly ILogger<TspRunner> _logger;
        public BruteForce(IRouteHelper routeHelper, ILogger<TspRunner> logger)
        {
            _routeHelper = routeHelper;
            _logger = logger;
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
                    _logger.LogInformation($"New Shortest route found, best route updated");
                }
            }

            string message = $"Best Route found: {bestRoute.PrintCoordinates()} with a total distance of {bestDistance}";
            _logger.LogInformation(message);
            System.Console.WriteLine(message);
        }
    }
}