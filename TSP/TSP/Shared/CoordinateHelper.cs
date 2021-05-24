namespace TSP
{
    using System;
    using System.Collections.Generic;
    public class CoordinateHelper : ICoordinateHelper
    {
        private readonly IConfigurationBuilderWrapper _configWrapper;
        public CoordinateHelper(IConfigurationBuilderWrapper wrapper)
        {
            _configWrapper = wrapper;
        }
        public IEnumerable<Coordinate> GenerateCoords(bool random = false, int size = 0)
        {
            return new List<Coordinate>();
        }

        public double TotalRouteDistance()
        {
            throw new NotImplementedException();
        }
        
        public double DistanceBetweenPoints(Coordinate one, Coordinate two)
        {   
            throw new NotImplementedException();
        }
    }
}