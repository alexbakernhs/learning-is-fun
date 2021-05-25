namespace TSP
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Prometheus;


    class Program
    {
        static void Main(string[] args)
        {
            var server = new MetricServer("localhost", 1234);
            server.Start();

            var host = AppStartup();
            // entry to run app
            host.Services.GetService<TspRunner>().Run();
        }

        private static IHost AppStartup()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Logger(lc => 
                    lc.Enrich.FromLogContext()
                    .Filter.ByExcluding(s => s.Level == Serilog.Events.LogEventLevel.Fatal && s.Level == Serilog.Events.LogEventLevel.Error)
                    .WriteTo.File($"OutputLogs/log{timestamp}.log")
                )
                .WriteTo.Logger(lc =>
                
                    lc.Enrich.FromLogContext()
                    .Filter.ByIncludingOnly(s => s.Level == Serilog.Events.LogEventLevel.Error && s.Level == Serilog.Events.LogEventLevel.Fatal)
                    .WriteTo.File("OutputLogs/ErrorLogs.log")
                )
                .CreateLogger();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddTransient<IConfigurationBuilderWrapper, ConfigurationBuilderWrapper>();
                    services.AddTransient<ICoordinateHelper, CoordinateHelper>();
                    services.AddTransient<IRouteHelper, RouteHelper>();
                    services.AddTransient<IRandomWrapper, RandomWrapper>();
                    services.AddTransient<IGeneticAlgorithmHelper, GeneticAlgorithmHelper>();
                    services.AddTransient<TspRunner>();
                })
                .UseSerilog()
                .Build();
            return host;
        }
    }
}
