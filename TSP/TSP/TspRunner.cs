namespace TSP
{
    using System.Collections.Generic;
    using System.Linq;
    using Serilog;
    using Microsoft.Extensions.Logging;
    using System.Diagnostics;
    using System;
    using CommandLine;

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

        public void Run(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(HandleParseError);
        }

        private void RunOptions(Options opts)
        {
            List<Coordinate> coords = new List<Coordinate>();
            if(opts.Random)
            {
                coords = _coordinateHelper.GenerateCoords(true, opts.Size, opts.MinX, opts.MaxX, opts.MinY, opts.MaxY).ToList();
            }
            else if(opts.AppSettings)
            {
                coords = _coordinateHelper.GenerateCoords().ToList();
            }
            else
            {
                _logger.LogCritical("Command Line options require one of -r (-random) or -a (-appsettings) so that it can generate coordinates");
                Console.WriteLine("Command Line options require one of -r (-random) or -a (-appsettings) so that it can generate coordinates");
                return;
            }


            if(opts.Brute)
            {
                BruteForce bruteForce = new BruteForce(_routeHelper, _logger);     
                Stopwatch sw  = new Stopwatch();
                sw.Start(); 
                _logger.LogInformation($"Brute Force Program started for coords: {coords.PrintCoordinates()}");
                bruteForce.Run(coords);
                sw.Stop();
                _logger.LogInformation($"Brute Force Finished running in {GetElapsedTimeFromTimeSpan(sw.Elapsed)}");
            }
            else if(opts.GA)
            {
                GA genetic = new GA(_gaHelper, _routeHelper, _logger);
                Stopwatch sw  = new Stopwatch();
                sw.Start(); 
                _logger.LogInformation($"Genetic Algorithm started for coords: {coords.PrintCoordinates()}");
                genetic.Run(coords);
                sw.Stop();
                _logger.LogInformation($"Genetic Algorithm Finished running in {GetElapsedTimeFromTimeSpan(sw.Elapsed)}");
            }
            else
            {
                _logger.LogCritical("Command Line options require one of -g (--genetic) or -b (--brute) so that it can run");
                Console.WriteLine("Command Line options require one of -g (--genetic) or -b (--brute) so that it can run");
            }
        }

        private void HandleParseError(IEnumerable<Error> errs)
        {
            _logger.LogCritical("Errors: {0} when trying to run from command line", errs);
            foreach(var err in errs)
            {
                _logger.LogCritical(err.ToString());
            }
            Console.WriteLine("Exit code {0}", -1);
        }

        private string GetElapsedTimeFromTimeSpan(TimeSpan ts)
        {
            return string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
        }
    }
}