namespace Tests
{
    using System;
    using Xunit;
    using Moq;
    using TSP;
    using System.Collections.Generic;
    using System.Linq;

    public class RouteHelperTests
    {
        [Fact]
        public void GenerateAllPossibleRoutes_Success()
        {
            //Arrange
            var routeHelper = new RouteHelper();
            var coordinates = new List<Coordinate>(){
                new Coordinate(0,0,true),
                new Coordinate(0,1,true),
                new Coordinate(1,1,true),
                new Coordinate(2,2,true),
                new Coordinate(4,4,true)
            };
            //Act
            var routes = routeHelper.GenerateAllPossibleRoutes(coordinates);

            //Assert
            Assert.Equal(120, routes.Count());
        }

                [Fact]
        public void TotalRouteDistance_Success()
        {
            //Arrange
            var helper = GetCoordinateHelper();
            List<Coordinate> route = new List<Coordinate>(){
                new Coordinate(0, 0, true),
                new Coordinate(0, 1, false),
                new Coordinate(1, 1, false),
                new Coordinate(0, 1, false)
            };

            //Act
            var distance = helper.TotalRouteDistance(route);

            //Assert
            Assert.Equal(4, distance);
        }

        [Fact]
        public void TotalRouteDistance_NullRoute_ArgumentException()
        {
            //Arrange
            var helper = GetCoordinateHelper();
            List<Coordinate> route = null;

            //Act & Assert
            Assert.Throws<ArgumentException>(() => helper.TotalRouteDistance(route));
        }
    }
}
