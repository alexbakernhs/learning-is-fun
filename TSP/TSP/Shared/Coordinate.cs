namespace TSP
{
    using System;
    public class Coordinate
    {
        public int X;
        public int Y;
        public bool IsStart;

        public Coordinate(int x, int y, bool isStart)
        {
            X = x;
            Y = y;
            IsStart = isStart;
        }
    }
}