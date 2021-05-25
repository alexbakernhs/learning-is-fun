namespace TSP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RandomWrapper : IRandomWrapper
    {
        public Coordinate Next(List<Coordinate> coords, int index)
        {
            Random random = new Random();
            Coordinate coordinate = new Coordinate(coords[index]);
            return coordinate;
        }

        public int Next(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}