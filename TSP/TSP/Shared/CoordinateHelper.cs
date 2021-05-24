namespace TSP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CoordinateHelper : ICoordinateHelper
    {
        private readonly IConfigurationBuilderWrapper _configWrapper;
        private readonly IRandomWrapper _randomWrapper;
        public CoordinateHelper(IConfigurationBuilderWrapper wrapper, IRandomWrapper random)
        {
            _configWrapper = wrapper;
            _randomWrapper = random;
        }
        public IEnumerable<Coordinate> GenerateCoords(bool random = false, int size = 0, int minX = 0, int maxX = 0, int minY = 0, int maxY = 0)
        {
            List<Coordinate> coords = new List<Coordinate>();
            if(!random)
            {
                coords =  _configWrapper.GetCoordinatesSection().Select(s => new Coordinate(s[0], s[1], false)).ToList();
                if(coords == null || coords.Count() < 1) throw new ArgumentException("No coords were found in the app settings");
            }
            else
            {
                if(((maxX - minX) + 1) * ((maxY - minY) + 1) < size) throw new ArgumentException("Size of graph does not match the number of expected coords");
                var allCoords = GenerateAllPossibleCoords(minX, maxX, minY, maxY);
                coords = new List<Coordinate>();
                for(int i = 0; i < size; i++)
                {
                    Random rand = new Random();
                    int index = rand.Next(0, allCoords.Count());
                    coords.Add(_randomWrapper.Next(allCoords, index));
                    allCoords.RemoveAt(index);
                }
            }
            if(coords != null && coords.Count() > 0) coords.First().IsStart = true;
            return coords;
        }

        private List<Coordinate> GenerateAllPossibleCoords(int minX, int maxX, int minY, int maxY)
        {
            var coords = new List<Coordinate>();
            for(int i = minX; i <= maxX; i++)
            {
                for(int j = minY; j <= maxY; j++)
                {
                    coords.Add(new Coordinate(i, j, false));
                }
            }
            return coords;
        }

        public double TotalRouteDistance(List<Coordinate> route)
        {
            if(route == null) throw new ArgumentException("Route cannot be null when trying to calculate distance");
            double distance = 0;
            for(int i = 0; i < route.Count(); i++)
            {
                var coordOne = route[i];
                var coordTwo = i == (route.Count() - 1) ? route[0] : route[i + 1];
                distance += DistanceBetweenPoints(coordOne, coordTwo);
            }
            return distance;
        }
        
        public double DistanceBetweenPoints(Coordinate one, Coordinate two)
        {   
            if(one == null) throw new ArgumentException("Coordinate one cant be null");
            if(two == null) throw new ArgumentException("Coordinate two cant be null");
            var aSq = Math.Pow((two.X - one.X), 2);
            var bSq = Math.Pow((two.Y -  one.Y), 2);
            return Math.Sqrt(aSq + bSq);
        }
    }
}