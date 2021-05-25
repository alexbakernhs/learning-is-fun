namespace TSP
{
    using System;
    using System.Collections.Generic;

    public class GA
    {
        public List<List<Coordinate>> CurrentPopulation;
        public double MutationRate = 0.01;
        public int PopulationSize = 100;
        public int GenerationSize = 100;
        public bool Elitism = true;
        public int TournamentSize = 5;

        public void Run(List<Coordinate> coords)
        {
            
        }
    }
}