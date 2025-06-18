using Microsoft.EntityFrameworkCore;
using SetsnReps.API.Infrastructure;
using SetsnReps.API.Mappings.Exercise;
using SetsnReps.Core.DTOs.Exercise;

namespace SetsnReps.API.Services.Exercise;

public class ExerciseService
{
    private readonly AppDbContext _dbContext;
    private const int PAGE_RECORDS = 20;
    
    public ExerciseService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Gets all exercises from the database.
    /// </summary>
    public async Task<IEnumerable<ExerciseResponse>> GetAllExercisesByPageAsync(
        int page, 
        CancellationToken cancellationToken = default)
    {
        if (page < 1)
            return null;
        
        var exercises = await _dbContext.Exercises
            .Include(x => x.EquipmentType)
            .Include(x => x.MuscleGroup)
            .AsNoTracking()
            .OrderBy(x => x.Id) // Garante ordenação consistente
            .Skip((page - 1) * PAGE_RECORDS)
            .Take(PAGE_RECORDS)
            .Select(x => x.ToExerciseResponse())
            .ToListAsync(cancellationToken);
        
        return exercises;
    }
    
    /// <summary>
    /// Gets an exercise by its ID from the database.
    /// </summary>
    /// <param name="id"></param>
    public async Task<ExerciseResponse?> GetExerciseByIdAsync(int id)
    {
        var exercise = await _dbContext.Exercises
            .Include(x => x.EquipmentType)
            .Include(x => x.MuscleGroup)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return exercise?.ToExerciseResponse();
    }
    
    /// <summary>
    /// Gets a list of exercises by a filter (like) on their name.
    /// </summary>
    /// <param name="name">The name or part of the name to search for</param>
    /// <param name="page">The page number (starting from 1)</param>
    /// <param name="cancellationToken">Token to cancel the operation</param>
    /// <returns>A collection of exercises matching the name criteria</returns>
    /// <exception cref="ArgumentException">Thrown when name is null or empty, or page is less than 1</exception>
    public async Task<IEnumerable<ExerciseResponse>> GetExercisesByNameAsync(
        string name,
        int page = 1,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;
        
        if (page < 1)
            return null;

        var exercises = await _dbContext.Exercises
            .Include(x => x.EquipmentType)
            .Include(x => x.MuscleGroup)
            .Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{name.ToLower()}%"))
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Skip((page - 1) * PAGE_RECORDS)
            .Take(PAGE_RECORDS)
            .Select(x => x.ToExerciseResponse())
            .ToListAsync(cancellationToken);

        return exercises;
    }
    
    /// <summary>
    /// Gets a list of exercises by a filter on their equipment type id.
    /// </summary>
    /// <param name="equipmentTypeId"></param>
    public async Task<IEnumerable<ExerciseResponse>> GetExercisesByEquipmentTypeIdAsync(int equipmentTypeId)
    {
        var exercises = await _dbContext.Exercises
            .Include(x => x.EquipmentType)
            .Include(x => x.MuscleGroup)
            .Where(x => x.EquipmentTypeId == equipmentTypeId)
            .AsNoTracking()
            .Select(x => x.ToExerciseResponse())
            .ToListAsync();

        return exercises;
    }
    
    /// <summary>
    ///  Gets a list of exercises by a filter on their primary muscle group id.
    /// </summary>
    /// <param name="primaryMuscleGroupId"></param>
    public async Task<IEnumerable<ExerciseResponse>> GetExercisesByPrimaryMuscleGroupIdAsync(int primaryMuscleGroupId)
    {
        var exercises = await _dbContext.Exercises
            .Include(x => x.EquipmentType)
            .Include(x => x.MuscleGroup)
            .Where(x => x.MuscleGroupId == primaryMuscleGroupId)
            .AsNoTracking()
            .Select(x => x.ToExerciseResponse())
            .ToListAsync();

        return exercises;
    }
}