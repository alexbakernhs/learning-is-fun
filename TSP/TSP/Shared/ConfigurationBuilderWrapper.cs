namespace TSP
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;

    public class ConfigurationBuilderWrapper : IConfigurationBuilderWrapper
    {
        public IEnumerable<int[]> GetCoordinatesSection()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            return config.GetSection("Coordinates").Get<IEnumerable<int[]>>();
        }
    }
}