using System.Collections.Generic;

namespace TSP
{
    public class TspRunner
    {
        private readonly ICoordinateHelper _coordinateHelper;
        private readonly IRouteHelper _routeHelper;
        public TspRunner(ICoordinateHelper coordinateHelper, IRouteHelper routeHelper)
        {
            _coordinateHelper = coordinateHelper;
            _routeHelper = routeHelper;
        }

        public void Run()
        {
            BruteForce bruteForce = new BruteForce(_routeHelper);
        }
    }
}