namespace TSP
{
    using System.Collections.Generic;
    using System.Linq;
    public static class CoordinateExtensions
    {
        public static bool DoesHaveDuplicates(this IEnumerable<Coordinate> coords)
        {
            foreach(var coord in coords)
            {
                if(coords.Where(s => s.X == coord.X && s.Y == coord.Y).Count() > 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}