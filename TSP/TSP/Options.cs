namespace TSP
{
    using CommandLine;
    public class Options
    {
        [Option('g', "genetic", Required = false, HelpText = "Run the program using a genetic algorithm")]
        public bool GA { get; set; }
    
        [Option('b', "brute", Required = false, HelpText = "Run the program using the mbrute force method")]
        public bool Brute { get; set; }

        [Option('a', "appsettings", Required = false, HelpText = "Run the program with coordinates set up in the appsettings.json file")]
        public bool AppSettings { get; set; }

        [Option('r', "random", Required = false, HelpText = "Run the program using a random set of coordinates. -s is required if points are random")]
        public bool Random { get; set; }

        [Option('s', "size", Default = 10, Required = false, HelpText = "The amount of coordinates you would like to plot on the graph")]
        public int Size { get; set; }

        [Option("minX", Default = 0, HelpText = "The minimum x value the coordinates will have.")]
        public int MinX { get; set; }

        [Option("minY", Default = 0, HelpText = "The minimum y value the coordinates will have.")]
        public int MinY { get; set; }

        [Option("maxX", Default = 10, HelpText = "The maximum x value the coordinates will have.")]
        public int MaxX { get; set; }

        [Option("maxY", Default = 10, HelpText = "The maximum y value the coordinates will have.")]
        public int MaxY { get; set; }

        [Option("popSize", Default = 100, HelpText = "The size of the populations the genertic algorithm will use.")]
        public int PopulationSize { get; set; }
        
        [Option("genSize", Default = 100, HelpText = "How many generations the Genetic Algorithm will run for")]
        public int GenerationSize { get; set; }

        [Option("mRate", Default = 0.01, HelpText = "The Mutation Rate of genes in the Genetic Algorithm")]
        public double MutationRate { get; set; }

        [Option("tSize", Default = 5, HelpText = "The size of the tournaments that will run in the Genetic Algorithm")]
        public int TournamentSize { get; set; }
    }
}