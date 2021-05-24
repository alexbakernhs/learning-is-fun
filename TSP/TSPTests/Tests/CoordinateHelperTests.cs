namespace Tests
{
    using System;
    using Xunit;
    using Moq;
    using TSP;
    using System.Collections.Generic;
    using System.Linq;

    public class UnitTest1
    {
        private Mock<IConfigurationBuilderWrapper> _configWrapper;
        private Mock<IRandomWrapper> _randomWrapper;
        private void Setup(bool overrideConfigWrapper = false, bool overrideRandomWrapper = false)
        {
            InitialiseMocks();
            MockSetups(overrideConfigWrapper, overrideRandomWrapper);
        }

        private void InitialiseMocks()
        {
            _configWrapper = new Mock<IConfigurationBuilderWrapper>();
            _randomWrapper = new Mock<IRandomWrapper>();
        }

        private void MockSetups(bool overrideConfigWrapper = false, bool overrideRandomWrapper = false)
        {
            if(!overrideConfigWrapper) _configWrapper.Setup(s => s.GetCoordinatesSection()).Returns(new List<int[]>{
                new int[2]{1,2},
                new int[2]{3,4},
                new int[2]{5,6},
                new int[2]{7,8}
            });
            if(!overrideRandomWrapper) _randomWrapper.SetupSequence(s => s.Next(It.IsAny<List<Coordinate>>(), It.IsAny<int>()))
                .Returns(new Coordinate(1, 2, false))
                .Returns(new Coordinate(3, 4, false))
                .Returns(new Coordinate(5, 6, false))
                .Returns(new Coordinate(7, 8, false));
        }

        private CoordinateHelper GetCoordinateHelper(bool overrideConfigWrapper = false, bool overrideRandomWrapper = false)
        {
            Setup();
            return new CoordinateHelper(_configWrapper.Object, _randomWrapper.Object);
        }
        
        [Fact]
        public void GenerateCoords_ReadFromAppSettings_Success()
        {
            //Arrange
            var helper = GetCoordinateHelper();

            //Act
            var coords = helper.GenerateCoords().ToList();

            //Assert
            Assert.Equal(4, coords.Count());
        }

        [Fact]
        public void GenerateCoords_ReadFromAppSettingsNoResults_ExceptionThrown()
        {
            //Arrange
            var helper = GetCoordinateHelper(overrideConfigWrapper: true);
            _configWrapper.Setup(s => s.GetCoordinatesSection()).Returns(new List<int[]>());

            //Act & Assert
            Assert.Throws<ArgumentException>(() => helper.GenerateCoords());
        }

        [Fact]
        public void GenerateCoords_GenerateRandom_Success()
        {
            //Arrange
            var helper = GetCoordinateHelper();

            //Act
            var coords = helper.GenerateCoords(true, 4, 0, 10, 0, 10);

            //Assert
            Assert.Equal(4, coords.Count());
        }

        [Fact]
        public void GenerateCoords_GenerateRandomTooManyPointsExpected_ArgumentException()
        {
            //Arrange
            var helper = GetCoordinateHelper(false, false);
            
            //Act & Assert
            Assert.Throws<ArgumentException>(() => helper.GenerateCoords(true, 15, 0, 2, 0, 3));
        }

        [Fact]
        public void GenerateCoords_AppSettingsCoords_IsFirstTrueOnFirst()
        {
            //Arrange
            var helper = GetCoordinateHelper(false, false);

            //Act
            var coords = helper.GenerateCoords();

            //Assert
            Assert.True(coords.First().IsStart);
        }
        
        [Fact]
        public void GenerateCoords_RandomGeneration_IsFirstTrueOnFirst()
        {
            //Arrange
            var helper = GetCoordinateHelper(false, false);

            //Act
            var coords = helper.GenerateCoords(true, 2, 0, 10, 0, 10);

            //Assert
            Assert.True(coords.First().IsStart);
        }

        [Fact]
        public void GenerateCoords_AnyGeneration_OnlyOneIsFirst()
        {
            //Arrange
            var helper = GetCoordinateHelper(false, false);

            //Act
            var coords = helper.GenerateCoords(true, 2, 0, 10, 0, 10);

            //Assert
            Assert.Single(coords.Where(w => w.IsStart));
        }

        [Fact]
        public void DistanceBetweenPoints_Success()
        {
            //Arrange
            var helper = GetCoordinateHelper();
            var coordOne = new Coordinate(0, 0, false);
            var coordTwo = new Coordinate(3, 4, false);

            //Act
            var distance = helper.DistanceBetweenPoints(coordOne, coordTwo);

            //Assert
            Assert.Equal(5, distance);
        }

        [Fact]
        public void DistanceBetweenPoints_CoordOneNull_ArgumentException()
        {
            //Arrange
            var helper = GetCoordinateHelper();
            Coordinate coordOne = null;
            Coordinate coordTwo = new Coordinate(3, 4, false);

            //Act && Assert
            Assert.Throws<ArgumentException>(() => helper.DistanceBetweenPoints(coordOne, coordTwo));
        }

        [Fact]
        public void DistanceBetweenPoints_CoordTwoNull_ArgumentException()
        {
            //Arrange
            var helper = GetCoordinateHelper();
            Coordinate coordOne = null;
            Coordinate coordTwo = new Coordinate(3, 4, false);

            //Act && Assert
            Assert.Throws<ArgumentException>(() => helper.DistanceBetweenPoints(coordOne, coordTwo));
        }
    }
}
