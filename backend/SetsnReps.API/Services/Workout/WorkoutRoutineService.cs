using SetsnReps.API.Infrastructure;
using SetsnReps.API.Mappings.Workout;
using SetsnReps.Core.DTOs.Workout;

namespace SetsnReps.API.Services.Workout;

public class WorkoutRoutineService
{
    private readonly AppDbContext _dbContext;
    
    public WorkoutRoutineService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    
    /// <summary>
    /// add a workout routine
    /// </summary>
    /// <param name="request">The request</param>
    /// <returns><see cref="WorkoutRoutineResponse"/></returns>
    public async Task<WorkoutRoutineResponse> AddWorkoutRoutineAsync(WorkoutRoutineRequest request)
    {
        // Validation: Must have a name and at least one workout exercise with at least one set.
        var workoutRoutine = request.ToWorkoutRoutine();

        _dbContext.WorkoutRoutines.Add(workoutRoutine);
        await _dbContext.SaveChangesAsync();

        return workoutRoutine.ToWorkoutRoutineResponse();
    }
    
}