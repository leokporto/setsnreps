using System.Data.SqlClient;
using System.Globalization;
using CsvHelper;
using Dapper;
using MegaGymImporter.Models;
using Microsoft.Extensions.Configuration;
using SetsnReps.Domain.Entities.Exercise;

namespace MegaGymImporter;

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
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        // Ler o CSV
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<MegaGymRow>().ToList();

        // 1. Importar MuscleGroups
        var uniqueBodyParts = records.Select(r => r.BodyPart).Distinct();
        // Verifica se ja tem registros na tabela, se tiver faz um truncate
        var muscleGroupCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM [MuscleGroups]");
        if (muscleGroupCount > 0)
        {
            await connection.ExecuteAsync("TRUNCATE TABLE [MuscleGroups]");
        }
        
        foreach (var bodyPart in uniqueBodyParts)
        {
            await connection.ExecuteAsync(@"
            INSERT INTO [MuscleGroups] ([Name])
            VALUES (@Name)",
                new { Name = bodyPart });
        }
        
        // Getting the Ids of the inserted MuscleGroups
        var muscleGroupIds = await connection.QueryAsync<MuscleGroup>("SELECT * FROM [MuscleGroups]");
        Dictionary <string, int> muscleGroupMap = muscleGroupIds.ToDictionary(x => x.Name, x => x.Id);   

        
        // 2. Importar EquipmentTypes
        // Verifica se ja tem registros na tabela, se tiver faz um truncate
        var equipmentCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM [EquipmentTypes]");
        if (equipmentCount > 0)
        {
            await connection.ExecuteAsync("TRUNCATE TABLE [EquipmentTypes]");
        }
        
        var uniqueEquipment = records.Select(r => r.Equipment).Distinct();
        foreach (var equipment in uniqueEquipment)
        {
            await connection.ExecuteAsync(@"
            INSERT INTO [EquipmentTypes] ([Name])
            VALUES (@Name)",
                new { Name = equipment });
        }
        
        // Getting the Ids of the inserted equipmenttypes
        var equipmentIds = await connection.QueryAsync<EquipmentType>("SELECT * FROM [EquipmentTypes]");
        Dictionary <string, int> equipmentMap = equipmentIds.ToDictionary(x => x.Name, x => x.Id);
        
        // 3. Importar Exercises
        // Verifica se ja tem registros na tabela, se tiver faz um truncate
        var exerciseCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM [Exercises]");
        if (exerciseCount > 0)
        {
            await connection.ExecuteAsync("TRUNCATE TABLE [Exercises[");
        }
        
        HashSet<Exercise> exercises = new();
        foreach (var record in records)
        {
            var exercise = new Exercise
            {
                Name = record.Title,
                PrimaryMuscleGroupId = muscleGroupMap[record.BodyPart],
                EquipmentTypeId = equipmentMap[record.Equipment]
            };
            exercises.Add(exercise);
        }
        
        var sql = @"
            INSERT INTO [Exercises] ([Name], [PrimaryMuscleGroupId], [EquipmentTypeId])
            VALUES (@Name, @PrimaryMuscleGroupId, @EquipmentTypeId)";

        foreach (Exercise item in exercises)
        {
            await connection.ExecuteAsync(sql, item);
        }

        
    }
}