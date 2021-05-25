namespace Tests
{
    using System;
    using Xunit;
    using Moq;
    using TSP;
    using System.Collections.Generic;
    using System.Linq;

    public class GeneticAlgorithmHelperTests
    {
        private Mock<IConfigurationBuilderWrapper> _config;
        private Mock<IRandomWrapper> _random;
        private CoordinateHelper _coordHelper;
        private Mock<IRouteHelper> _routeHelper;
        private GeneticAlgorithmHelper GetAlgoHelper()
        {
            InitialiseMocks();
            MockSetups();
            return new GeneticAlgorithmHelper(_routeHelper.Object, _random.Object);
        }

        private void InitialiseMocks()
        {
            _routeHelper = new Mock<IRouteHelper>();
            _config = new Mock<IConfigurationBuilderWrapper>();
            _random = new Mock<IRandomWrapper>();
            _coordHelper = new CoordinateHelper(_config.Object, _random.Object);
        }

        private void MockSetups()
        {
            _routeHelper.Setup(s => s.TotalRouteDistance(It.IsAny<List<Coordinate>>())).Returns((List<Coordinate> coords) => {
                double distance = 0;
                for(int i = 0; i < coords.Count(); i++)
                {
                    var coordOne = coords[i];
                    var coordTwo = i == (coords.Count() - 1) ? coords[0] : coords[i + 1];
                    distance += _coordHelper.DistanceBetweenPoints(coordOne, coordTwo);
                }
                return distance;
            });
        }

        [Fact]
        public void GenerateInitialPopulation_Success()
        {
            //Arrange
            var helper = GetAlgoHelper();
            var coords = new List<Coordinate>()
            {
                new Coordinate(0,0,true),
                new Coordinate(1,1,false),
                new Coordinate(2,2,false),
                new Coordinate(3,3,false),
                new Coordinate(4,4,false)
            };
            int popSize = 10;

            //Act
            var population = helper.GenerateInitialPopulation(coords, popSize);
            
            //Assert
            Assert.Equal(10, population.Count());
        }

        [Fact]
        public void GenerateInitialPopulation_CoordsNull_ArgumentException()
        {
            //Arrange
            var helper = GetAlgoHelper();
            List<Coordinate> coords = null;
            int popSize = 10;

            //Act & Assert
            Assert.Throws<ArgumentException>(() => helper.GenerateInitialPopulation(coords, popSize));
        }

        [Fact]
        public void GenerateInitialPopulation_populationSizeSmallerThan3_ArgumentException()
        {
            //Arrange
            var helper = GetAlgoHelper();
            var coords = new List<Coordinate>()
            {
                new Coordinate(0,0,true),
                new Coordinate(1,1,false),
                new Coordinate(2,2,false),
                new Coordinate(3,3,false),
                new Coordinate(4,4,false)
            };
            int popSize = 2;

            //Act && Assert
            Assert.Throws<ArgumentException>(() => helper.GenerateInitialPopulation(coords, popSize));
        }

        [Fact]
        public void GetFittest_Success()
        {
            //Arrange
            var helper = GetAlgoHelper();
            var population = new List<List<Coordinate>>()
            {
                new List<Coordinate>{      
                    new Coordinate(1,2,true),
                    new Coordinate(8,2,false),
                    new Coordinate(3,4,false),
                    new Coordinate(6,3,false)
                },
                new List<Coordinate>{      
                    new Coordinate(1,2,true),
                    new Coordinate(3,4,false),
                    new Coordinate(8,2,false),
                    new Coordinate(6,3,false)
                },
            };

            //Act
            var fittest = helper.GetFittest(population);

            //Assert
            Assert.True(fittest.Equals(population[1]));
        }

        [Fact]
        public void GetFittest_PopulationNull_ArgumentException()
        {
            //Arrange
            var helper = GetAlgoHelper();
            List<List<Coordinate>> population = null;

            //Act && Assert
            Assert.Throws<ArgumentException>(() => helper.GetFittest(population));
        }

        [Fact]
        public void RunTournament_PopulationNull_ArgumentException()
        {
            //Arrange
            var helper = GetAlgoHelper();
            List<List<Coordinate>> population = null;

            //Act && Assert
            Assert.Throws<ArgumentException>(() => helper.RunTournament(population, 10));
        }   

        [Fact]
        public void RunTournament_TorunamentSizeLessThan2_ArgumentException()
        {
            //Arrange
            var helper = GetAlgoHelper();
            var population = new List<List<Coordinate>>()
            {
                new List<Coordinate>{      
                    new Coordinate(1,2,true),
                    new Coordinate(8,2,false),
                    new Coordinate(3,4,false),
                    new Coordinate(6,3,false)
                },
                new List<Coordinate>{      
                    new Coordinate(1,2,true),
                    new Coordinate(3,4,false),
                    new Coordinate(8,2,false),
                    new Coordinate(6,3,false)
                },
            };

            //Act && Assert
            Assert.Throws<ArgumentException>(() => helper.RunTournament(population, 1));
        }

        [Fact]
        public void RunCrossover_Success()
        {
            //Arrange
            var helper = GetAlgoHelper();
            _random.SetupSequence(s => s.Next(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(2)
            .Returns(4);
            var parentOne = new List<Coordinate>
            {
                new Coordinate(0, 0, true),
                new Coordinate(1, 1, false),
                new Coordinate(2, 2, false),
                new Coordinate(3, 3, false),
                new Coordinate(4, 4, false),
                new Coordinate(5, 5, false),
            };
            var parentTwo = new List<Coordinate>
            {
                new Coordinate(6, 6, true),
                new Coordinate(7, 7, false),
                new Coordinate(8, 8, false),
                new Coordinate(9, 9, false),
                new Coordinate(10, 10, false),
                new Coordinate(11, 11, false),
            };
            var expectedChild = new List<Coordinate>
            {
                new Coordinate(0, 0, true),
                new Coordinate(1, 1, false),
                new Coordinate(8, 8, false),
                new Coordinate(9, 9, false),
                new Coordinate(10, 10, false),
                new Coordinate(5, 5, false),
            };

            //Act
            var child = helper.RunCrossover(parentOne, parentTwo);

            var result = child.Equals(coordsComparison: expectedChild);

            //Assert
            Assert.True(result);
        }
    }
}
