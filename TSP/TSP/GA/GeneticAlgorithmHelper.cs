namespace TSP
{
    using System;
    using System.Collections.Generic;

    public class GeneticAlgorithmHelper : IGeneticAlgorithmHelper
    {
        private readonly IRouteHelper _routeHelper;

        public GeneticAlgorithmHelper(IRouteHelper _routeHelper)
        {
            
        }
        public List<List<Coordinate>> GenerateInitialPopulation(List<Coordinate> coords, int populationSize)
        {
            throw new NotImplementedException();
        }

        public List<List<Coordinate>> EvolvePopulation(List<List<Coordinate>> population)
        {
            throw new NotImplementedException();
        }

        public List<Coordinate> GetFittest(List<List<Coordinate>> population)
        {
            throw new NotImplementedException();
        }

        public List<Coordinate> RunCrossover(List<Coordinate> parentOne, List<Coordinate> parentTwo)
        {
            throw new NotImplementedException();
        }

        public List<Coordinate> RunMutation(List<List<Coordinate>> population, double mutationRate)
        {
            throw new NotImplementedException();
        }

        public List<Coordinate> RunTournament(List<List<Coordinate>> population, int tournamentSize)
        {
            throw new NotImplementedException();
        }
    }
}