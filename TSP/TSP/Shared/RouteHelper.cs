namespace TSP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Prometheus;

    public class RouteHelper : IRouteHelper
    {
        private readonly ICoordinateHelper _coordinateHelper;
        private static readonly Counter RouteCounter = Metrics.CreateCounter("route_counter", "Route Counter");
        public RouteHelper(ICoordinateHelper coordinateHelper)
        {
            _coordinateHelper = coordinateHelper;
        }
        public List<List<Coordinate>> GenerateAllPossibleRoutes(List<Coordinate> coords)
        {
            List<List<Coordinate>> routes = new List<List<Coordinate>>();
            List<int> indexes = new List<int>();
            for(int i = 0; i < coords.Count; i++)
            {
                indexes.Add(i);
            }
            
            foreach (var permu in Permutate(indexes, indexes.Count))
            {
                List<Coordinate> route = new List<Coordinate>();
                foreach (var i in permu)
                {
                    route.Add(coords[i]);
                }
                RouteCounter.Inc();
                routes.Add(route);
            }
            return routes;
        }
        
        private void RotateRight(List<int> sequence, int count)
        {
            int tmp = sequence[count-1];
            sequence.RemoveAt(count - 1);
            sequence.Insert(0, tmp);
        }
        
        private IEnumerable<List<int>> Permutate(List<int> sequence, int count)
        {
            if (count == 1) yield return sequence;
            else
            {
                for (int i = 0; i < count; i++)
                {
                    foreach (var perm in Permutate(sequence, count - 1))
                        yield return perm;
                    RotateRight(sequence, count);
                }
            }
        }

        public double TotalRouteDistance(List<Coordinate> route)
        {
            if(route == null) throw new ArgumentException("Route cannot be null when trying to calculate distance");
            double distance = 0;
            for(int i = 0; i < route.Count(); i++)
            {
                var coordOne = route[i];
                var coordTwo = i == (route.Count() - 1) ? route[0] : route[i + 1];
                distance += _coordinateHelper.DistanceBetweenPoints(coordOne, coordTwo);
            }
            return distance;
        }
    }
}
