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
                if(!coords[i].IsStart)
                {
                    indexes.Add(i);
                }
            }
            
            foreach (var permu in Permutate(indexes, indexes.Count))
            {
                List<Coordinate> route = new List<Coordinate>();
                Coordinate first = coords.FirstOrDefault(s => s.IsStart);
                route.Add(first);
                foreach (var i in permu)
                {
                    route.Add(coords[i]);
                }
                route.Add(first);
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
            for(int i = 0; i < route.Count() - 1; i++)
            {
                var coordOne = route[i];
                var coordTwo = route[i + 1];
                distance += _coordinateHelper.DistanceBetweenPoints(coordOne, coordTwo);
            }
            return distance;
        }

        public List<Coordinate> GenerateRandomRoute(List<Coordinate> coords)
        {
            var route = new List<Coordinate>();
            var start = coords.FirstOrDefault(s => s.IsStart);
            route.Add(start);

            List<int> indexes = new List<int>();
            for(int i = 0; i < coords.Count; i++)
            {
                if(!coords[i].IsStart)
                {
                    indexes.Add(i);
                }
            }
            Random random = new Random();
            var count = indexes.Count();
            while(indexes.Count > 0)
            {
                int index = random.Next(0, indexes.Count);
                route.Add(coords[indexes[index]]);
                indexes.RemoveAt(index);
            }
            route.Add(start);

            return route;
        }
    }
}
