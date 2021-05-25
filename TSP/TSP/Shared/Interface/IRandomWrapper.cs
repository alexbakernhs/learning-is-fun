namespace TSP
{
    using System.Collections.Generic;
    public interface IRandomWrapper
    {
        Coordinate Next(List<Coordinate> coords, int index);
        int Next(int min, int max);
    }
}