using System.Collections.Generic;

namespace TSP
{
    public interface IConfigurationBuilderWrapper
    {
        IEnumerable<int[]> GetCoordinatesSection();
    }
}