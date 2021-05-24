namespace TSP
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using Microsoft.Extensions.Configuration;
    using System.Linq;
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            List<Coordinate> coords = config.GetSection("Coordinates").Get<IEnumerable<int[]>>().Select(s => new Coordinate(s[0], s[1], false)).ToList();
            coords.First().IsStart = true;
        }
    }
}
