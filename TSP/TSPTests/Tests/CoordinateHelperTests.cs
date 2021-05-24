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
            if(!overrideRandomWrapper) _randomWrapper.SetupSequence(s => s.Next(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(2)
                .Returns(3)
                .Returns(4)
                .Returns(5)
                .Returns(6)
                .Returns(7)
                .Returns(8);
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
        public void GenerateCoords_GenerateRandom_NoDuplicates()
        {
            //Arrange
            var helper = GetCoordinateHelper(false, true);
            _randomWrapper.SetupSequence(s => s.Next(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(2)
                .Returns(1)
                .Returns(2)
                .Returns(3)
                .Returns(4);

            //Act
            var coords = helper.GenerateCoords(true, 2, 0, 4, 0, 4);

            //Assert
            Assert.False(coords.DoesHaveDuplicates());
        }
    }
}
