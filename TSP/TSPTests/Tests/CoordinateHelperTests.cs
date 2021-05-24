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
        private void Setup(bool overrideConfigWrapper = false)
        {
            InitialiseMocks();
            MockSetups(overrideConfigWrapper);
        }

        private void InitialiseMocks()
        {
            _configWrapper = new Mock<IConfigurationBuilderWrapper>();
        }

        private void MockSetups(bool overrideConfigWrapper = false)
        {
            if(!overrideConfigWrapper) _configWrapper.Setup(s => s.GetCoordinatesSection()).Returns(new List<int[]>{
                new int[2]{1,2},
                new int[2]{3,4},
                new int[2]{5,6},
                new int[2]{7,8}
            });
        }

        private CoordinateHelper GetCoordinateHelper(bool overrideConfigWrapper = false)
        {
            Setup();
            return new CoordinateHelper(_configWrapper.Object);
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
    }
}
