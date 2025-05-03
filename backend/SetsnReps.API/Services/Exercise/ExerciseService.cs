using Microsoft.EntityFrameworkCore;
using SetsnReps.API.Infrastructure;
using SetsnReps.API.Mappings.Exercise;
using SetsnReps.Core.DTOs.Exercise;

namespace SetsnReps.API.Services.Exercise;

public class ExerciseService
{
    private readonly AppDbContext _dbContext;

    public ExerciseService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Gets all exercises from the database.
    /// </summary>
    public async Task<IEnumerable<ExerciseResponse>> GetAllExercisesAsync()
    {
        var exercises = await _dbContext.Exercises
            .Include(x => x.EquipmentType)
            .Include(x => x.MuscleGroup)
            .AsNoTracking()
            .Select(x => x.ToExerciseResponse())
            .ToListAsync();

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
    /// <param name="name"></param>
    public async Task<IEnumerable<ExerciseResponse>> GetExercisesByNameAsync(string name)
    {
        var exercises = await _dbContext.Exercises
            .Include(x => x.EquipmentType)
            .Include(x => x.MuscleGroup)
            .Where(x => x.Name.Contains(name))
            .AsNoTracking()
            .Select(x => x.ToExerciseResponse())
            .ToListAsync();

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