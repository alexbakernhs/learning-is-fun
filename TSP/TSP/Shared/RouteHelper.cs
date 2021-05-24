namespace TSP
{
    using System.Collections.Generic;
    public class RouteHelper : IRouteHelper
    {
        public List<List<Coordinate>> GenerateAllPossibleRoutes(List<Coordinate> coords)
        {
            List<int> indexes = new List<int>();
            for(int i = 0; i < coords.Count; i++)
            {
                indexes.Add(i);
            }
            
            throw new System.NotImplementedException();
        }
    }
}