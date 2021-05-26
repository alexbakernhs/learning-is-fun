namespace TSP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Logging;

    public class GA
    {
        private readonly IGeneticAlgorithmHelper _gaHelper;
        private readonly IRouteHelper _routeHelper;
        private readonly ILogger<TspRunner> _logger;

        public GA(IGeneticAlgorithmHelper gaHelper, IRouteHelper routeHelper, ILogger<TspRunner> logger)
        {
            _gaHelper = gaHelper;
            _routeHelper = routeHelper;
            _logger = logger;
        }
        public List<List<Coordinate>> CurrentPopulation;
        public double MutationRate = 0.01;
        public int PopulationSize = 100;
        public int GenerationSize = 100;
        public bool Elitism = false;
        public int TournamentSize = 5;

        public void Run(List<Coordinate> coords)
        {
            var fittest = new List<Coordinate>();

            for(int i = 0; i < GenerationSize; i++)
            {
                if(CurrentPopulation == null)
                {
                    CurrentPopulation = _gaHelper.GenerateInitialPopulation(coords, PopulationSize);
                }
                fittest = _gaHelper.GetFittest(CurrentPopulation);
                
                _logger.LogInformation($"{fittest.PrintCoordinates()} {_routeHelper.TotalRouteDistance(fittest)}");

                CurrentPopulation = EvolvePopulation(CurrentPopulation);
            }
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
                var child = RunCrossoverOnTournamentWinners(population, TournamentSize);
                newPopulation.Add(child);
            }

            newPopulation = _gaHelper.RunMutation(newPopulation, 0.05);
            return newPopulation;
        }

        private List<Coordinate> RunCrossoverOnTournamentWinners(List<List<Coordinate>> population, int tournamentSize)
        {
            List<Coordinate> parentOne = _gaHelper.RunTournament(population, 5);
            List<Coordinate> parentTwo = _gaHelper.RunTournament(population, 5);

            return _gaHelper.RunCrossover(parentOne, parentTwo);
        }
    }
}