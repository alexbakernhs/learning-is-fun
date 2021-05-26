namespace TSP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GeneticAlgorithmHelper : IGeneticAlgorithmHelper
    {
        private readonly IRouteHelper _routeHelper;
        private readonly IRandomWrapper _randomWrapper;

        public GeneticAlgorithmHelper(IRouteHelper routeHelper, IRandomWrapper randomWrapper)
        {
            _routeHelper = routeHelper;
            _randomWrapper = randomWrapper;
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
            var crossoverRoute = new List<Coordinate>();
            int indexOne = _randomWrapper.Next(1, parentOne.Count() - 1);
            int indexTwo = -1;
            while(indexOne == indexTwo || indexTwo == -1)
            {
                indexTwo = _randomWrapper.Next(1,parentOne.Count() - 1);
            }


            /*
                Remove section of parent one using the indexes

                Get the remaining unique coordinates from parent two

                for i is less than the parents size

                if i is between index one and index two then add from the sub section
                else add from the unique remainders;
            */

            var subsection = parentOne
                .Where((item, index) => index != 0 && index != parentOne.Count - 1 && (indexOne < indexTwo ? index >= indexOne && index <= indexTwo : index >= indexTwo && index <= indexOne))
                .ToList();
            
            var uniqueRemainders = parentTwo
                .Where(x => !x.IsInList(subsection))
                .ToList();
            
            for(int i = 0; i < uniqueRemainders.Count;)
            {
                if(indexOne < indexTwo)
                {
                    if(crossoverRoute.Count >= indexOne && crossoverRoute.Count <= indexTwo)  //if part of subsection
                    {
                       crossoverRoute.AddRange(subsection);
                       continue;
                    }else
                    {
                        crossoverRoute.Add(uniqueRemainders[i]);
                    }
                }else{
                    if(crossoverRoute.Count <= indexOne && crossoverRoute.Count >= indexTwo) //if part of subsection
                    {
                       crossoverRoute.AddRange(subsection);  
                       continue;
                    }else
                    {
                        crossoverRoute.Add(uniqueRemainders[i]);
                    }
                }
                i++;
            }
            
            crossoverRoute.All(s => s.IsStart = false);
            crossoverRoute.FirstOrDefault(s => s.IsStart = true);
            return crossoverRoute;
        }

        public List<List<Coordinate>> RunMutation(List<List<Coordinate>> population, double mutationRate)
        {
            var mutated = new List<List<Coordinate>>();
            foreach(var route in population)
            {
                Random random = new Random();
                if(random.NextDouble() < mutationRate)
                {
                    int indexOne = random.Next(1, route.Count - 1); //Ignore first and last
                    int indexTwo = -1;
                    while(indexOne == indexTwo || indexTwo == -1)
                    {
                        indexTwo = _randomWrapper.Next(1,route.Count - 1);
                    }
                    var newRoute = new List<Coordinate>(route);
                    newRoute[indexTwo] = route[indexOne];
                    newRoute[indexOne] = route[indexTwo];
                    mutated.Add(newRoute);
                }else{
                    mutated.Add(route);
                }
            }

            return mutated;
        }

        public List<Coordinate> RunTournament(List<List<Coordinate>> population, int tournamentSize)
        {
            if(population == null) throw new ArgumentException("Population used to run a tournament cannot be null");
            if(tournamentSize < 2) throw new ArgumentException("Torunament size must be greater than 1 otherwise it is pointless");
            
            var tournamentPopulation = GenerateTournamentPopulation(population, tournamentSize);
            return GetFittest(tournamentPopulation);
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