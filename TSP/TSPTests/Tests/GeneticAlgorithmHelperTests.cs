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
        private Mock<IRouteHelper> _routeHelper;
        private Mock<IConfigurationBuilderWrapper> _config;
        private Mock<IRandomWrapper> _random;
        private CoordinateHelper _coordHelper;

        private GeneticAlgorithmHelper GetAlgoHelper()
        {
            InitialiseMocks();
            MockSetups();
            return new GeneticAlgorithmHelper(_routeHelper.Object);
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

            //Act
            var population = helper.GenerateInitialPopulation(coords, 10);
            
            //Assert
            Assert.Equal(10, population.Count());
        }
    }
}
