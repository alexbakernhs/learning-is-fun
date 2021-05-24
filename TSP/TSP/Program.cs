namespace TSP
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using Microsoft.Extensions.Configuration;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;

    class Program
    {
        static void Main(string[] args)
        {

            IServiceCollection services = new ServiceCollection();

            services.AddTransient<IConfigurationBuilderWrapper, ConfigurationBuilderWrapper>();
            services.AddTransient<ICoordinateHelper, CoordinateHelper>();

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            // entry to run app
            serviceProvider.GetService<TspRunner>().Run();


            
        }
    }
}
