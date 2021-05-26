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

        private readonly IGeneticAlgorithmHelper _gaHelper;

        public TspRunner(ICoordinateHelper coordinateHelper, IRouteHelper routeHelper, ILogger<TspRunner> logger, IGeneticAlgorithmHelper gaHelper)
        {
            _coordinateHelper = coordinateHelper;
            _routeHelper = routeHelper;
            _logger = logger;
            _gaHelper = gaHelper;
        }

        public void Run()
        {
            BruteForce bruteForce = new BruteForce(_routeHelper, _logger);
            //var coords = _coordinateHelper.GenerateCoords().ToList(); //Coords from appsettings
            var coords = _coordinateHelper.GenerateCoords(true, 50, 0, 20, 0, 20).ToList(); //Coords made at random

            Stopwatch bruteForceSw = new Stopwatch();
            bruteForceSw.Start();
            
            _logger.LogInformation($"Brute Force Program begin running for coords: {coords.PrintCoordinates()}");
            //bruteForce.Run(coords);
            bruteForceSw.Stop();
            _logger.LogInformation($"Brute Force Finished running in {GetElapsedTimeFromTimeSpan(bruteForceSw.Elapsed)}");
        
            GA genetic = new GA(_gaHelper, _routeHelper);

            _logger.LogInformation($"GA begin running");
            genetic.Run(coords);
            
        }

        private string GetElapsedTimeFromTimeSpan(TimeSpan ts)
        {
            return string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
        }
    }
}