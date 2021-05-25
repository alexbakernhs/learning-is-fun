namespace TSP
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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

        public static string PrintCoordinates(this List<Coordinate> coords)
        {
            StringBuilder builder = new StringBuilder();
            foreach(var coord in coords)
            {
                builder.Append(coord.PrintCoordinate());
            }
            return  builder.ToString();
        }

        public static string PrintCoordinate(this Coordinate coord)
        {
            return $"({coord.X.ToString()}, {coord.Y.ToString()})";
        }
    }
}