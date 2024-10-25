using DIExplorer;

// Ensure a folder path argument is provided
if (args.Length < 1)
{
    Console.WriteLine("Usage: DIExplorer <path to target folder>");
    return;
}

var filePath = args[0];

// Create an instance of the analyzer and run it on the provided folder
var analyzer = new DependencyInjectionAnalyzer();
analyzer.AnalyzeDependenciesInFolder(filePath);
analyzer.DisplayDependencyGraph();