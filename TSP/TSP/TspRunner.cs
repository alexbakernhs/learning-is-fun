namespace TSP
{
    using System.Collections.Generic;
    using System.Linq;
    using Serilog;
    using Microsoft.Extensions.Logging;
    using System.Diagnostics;
    using System;

    public class TspRunner
    {
        private readonly ICoordinateHelper _coordinateHelper;
        private readonly IRouteHelper _routeHelper;
        private readonly ILogger<TspRunner> _logger;

        public TspRunner(ICoordinateHelper coordinateHelper, IRouteHelper routeHelper, ILogger<TspRunner> logger)
        {
            _coordinateHelper = coordinateHelper;
            _routeHelper = routeHelper;
            _logger = logger;
        }

        public void Run()
        {

            BruteForce bruteForce = new BruteForce(_routeHelper, _logger);
            var coords = _coordinateHelper.GenerateCoords().ToList(); //Coords from appsettings
            //var coords = _coordinateHelper.GenerateCoords(true, 10, 0, 10, 0, 10).ToList(); //Coords made at random

            Stopwatch bruteForceSw = new Stopwatch();
            bruteForceSw.Start();
            
            _logger.LogInformation($"Brute Force Program begin running for coords: {coords.PrintCoordinates()}");
            bruteForce.Run(coords);
            bruteForceSw.Stop();
            _logger.LogInformation($"Brute Force Finished running in {GetElapsedTimeFromTimeSpan(bruteForceSw.Elapsed)}");
        }

        private string GetElapsedTimeFromTimeSpan(TimeSpan ts)
        {
            return string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
        }
    }
}