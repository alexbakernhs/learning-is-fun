namespace TSP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GA
    {
        private readonly IGeneticAlgorithmHelper _gaHelper;
        public GA(IGeneticAlgorithmHelper gaHelper)
        {
            _gaHelper = gaHelper;
        }
        public List<List<Coordinate>> CurrentPopulation;
        public double MutationRate = 0.01;
        public int PopulationSize = 100;
        public int GenerationSize = 100;
        public bool Elitism = true;
        public int TournamentSize = 5;

        public void Run(List<Coordinate> coords)
        {
            
        }

        private List<List<Coordinate>> EvolvePopulation(List<List<Coordinate>> population)
        {
            var newPopulation = new List<List<Coordinate>>();
            if(Elitism)
            {
                var fittest = _gaHelper.GetFittest(population);
                newPopulation.Add(fittest);
            }

            for(int i = Elitism ? 1 : 0; i < population.Count(); i++)
            {
                List<Coordinate> parentOne = _gaHelper.RunTournament(population, 5);
                List<Coordinate> parentTwo = _gaHelper.RunTournament(population, 5);

                List<Coordinate> child = _gaHelper.RunCrossover(parentOne, parentTwo);
                newPopulation.Add(child);
            }

            newPopulation = _gaHelper.RunMutation(newPopulation, 0.05);
            return newPopulation;
        }
    }
}