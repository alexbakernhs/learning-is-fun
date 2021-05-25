namespace TSP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GeneticAlgorithmHelper : IGeneticAlgorithmHelper
    {
        private readonly IRouteHelper _routeHelper;

        public GeneticAlgorithmHelper(IRouteHelper routeHelper)
        {
            _routeHelper = routeHelper;
        }
        public List<List<Coordinate>> GenerateInitialPopulation(List<Coordinate> coords, int populationSize)
        {
            if(coords == null || coords.Count < 0) throw new ArgumentException("Coords is null or empty");
            if(populationSize < 3) throw new ArgumentException("Population size is too small");
            var population = new List<List<Coordinate>>();
            for(int i = 0; i < populationSize; i++)
            {
                population.Add(_routeHelper.GenerateRandomRoute(coords));
            }
            return population;
        }

        public List<Coordinate> GetFittest(List<List<Coordinate>> population)
        {
            if(population == null) throw new ArgumentException("Population cannot be null");
            double? fittest = null;
            List<Coordinate> fittestRoute = new List<Coordinate>();
            foreach(var route in population)
            {
                var routeDistance = _routeHelper.TotalRouteDistance(route);
                if(fittest == null || routeDistance < fittest)
                {
                    fittest = routeDistance;
                    fittestRoute = route;
                }
            }
            return fittestRoute;
        }

        public List<Coordinate> RunCrossover(List<Coordinate> parentOne, List<Coordinate> parentTwo)
        {
            throw new NotImplementedException();
        }

        public List<List<Coordinate>> RunMutation(List<List<Coordinate>> population, double mutationRate)
        {
            throw new NotImplementedException();
        }

        public List<Coordinate> RunTournament(List<List<Coordinate>> population, int tournamentSize)
        {
            throw new NotImplementedException();
        }

        private List<List<Coordinate>> GenerateTournamentPopulation(List<List<Coordinate>> population, int tournamentSize)
        {
            var tournamentPopulation = new List<List<Coordinate>>();
            var indexes = new List<int>();
            for(int i = 0; i < population.Count; i++)
            {
                indexes.Add(i);
            }
            Random random = new Random();
            for(int i = 0; i < tournamentSize; i++)
            {
                int index = random.Next(0, indexes.Count);
        
                tournamentPopulation.Add(population[index]);

                indexes.RemoveAt(index);
            }
            return tournamentPopulation;
        }
    }
}