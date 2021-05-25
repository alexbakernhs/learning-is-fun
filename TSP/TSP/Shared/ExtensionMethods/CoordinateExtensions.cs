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

        public static bool Equals(this Coordinate coord, Coordinate coordComparison)
        {
            if(coord.X != coordComparison.X || coord.Y != coordComparison.Y) return false;
            else return true;
        }

        public static bool IsInList(this Coordinate coordinate, List<Coordinate> comparison)
        {
            foreach(var coord in comparison)
            {
                if(coordinate.Equals(coord)) return true;
            }
            return false;
        }

        public static bool Equals(this List<Coordinate> coords, List<Coordinate> coordsComparison)
        {
            int counter = 0;
            foreach(var coord in coords)
            {
                if(coord.X != coordsComparison[counter].X || coord.Y != coordsComparison[counter].Y) return false;
                counter++;
            }
            return true;
        }
    }
}