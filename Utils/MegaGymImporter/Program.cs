using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MegaGymImporter;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets<Program>()
            .AddEnvironmentVariables();

        IConfiguration configuration = builder.Build();

        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);

        // Aqui você pode injetar seus serviços
        services.AddTransient<ExerciseImporterSvc>(); // Sua classe que fará a importação

        var serviceProvider = services.BuildServiceProvider();

        // Usando o serviço
        var importer = serviceProvider.GetRequiredService<ExerciseImporterSvc>();
        await importer.ImportExercisesFromCsv(@".\Csv\megaGymDataset.csv");


    }
    
    
}