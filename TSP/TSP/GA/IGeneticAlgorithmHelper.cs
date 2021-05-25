namespace TSP
{
    using System.Collections.Generic;
    public interface IGeneticAlgorithmHelper
    {
        List<List<Coordinate>> GenerateInitialPopulation(List<Coordinate> coords, int populationSize);
        List<List<Coordinate>> EvolvePopulation(List<List<Coordinate>> population);
        List<Coordinate> RunCrossover(List<Coordinate> parentOne, List<Coordinate> parentTwo);
        List<Coordinate> RunTournament(List<List<Coordinate>> population, int tournamentSize);
        List<Coordinate> RunMutation(List<List<Coordinate>> population, double mutationRate);
        List<Coordinate> GetFittest(List<List<Coordinate>> population);
    }
}