using System.Globalization;
using CsvHelper;
using Dapper;
using MegaGymImporter.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

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

public class ExerciseImporterSvc
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public ExerciseImporterSvc(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    
    public async Task ImportExercisesFromCsv(string filePath)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        // Ler o CSV
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<MegaGymRow>().ToList();

        // 1. Importar MuscleGroups
        var uniqueBodyParts = records.Select(r => r.BodyPart).Distinct();
        foreach (var bodyPart in uniqueBodyParts)
        {
            await connection.ExecuteAsync(@"
            INSERT INTO ""MuscleGroups"" (""Name"")
            VALUES (@Name)",
                new { Name = bodyPart });
        }

        // 2. Importar EquipmentTypes
        var uniqueEquipment = records.Select(r => r.Equipment).Distinct();
        foreach (var equipment in uniqueEquipment)
        {
            await connection.ExecuteAsync(@"
            INSERT INTO ""EquipmentTypes"" (""Name"")
            VALUES (@Name)",
                new { Name = equipment });
        }

        // 3. Importar Exercises
        var sql = @"
        WITH muscle_group_id AS (
            SELECT ""Id"" FROM ""MuscleGroups"" WHERE ""Name"" = @BodyPart
        ),
        equipment_id AS (
            SELECT ""Id"" FROM ""EquipmentTypes"" WHERE ""Name"" = @Equipment
        )
        INSERT INTO ""Exercises"" (""Name"", ""MuscleGroupId"", ""EquipmentTypeId"")
        SELECT @Title, 
               (SELECT ""Id"" FROM muscle_group_id),
               (SELECT ""Id"" FROM equipment_id)";

        foreach (var record in records)
        {
            await connection.ExecuteAsync(sql, record);
        }
    }
}
