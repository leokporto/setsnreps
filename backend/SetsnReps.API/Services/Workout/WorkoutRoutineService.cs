using Microsoft.EntityFrameworkCore;
using SetsnReps.API.Infrastructure;
using SetsnReps.API.Mappings.Workout;
using SetsnReps.Core.DTOs.Workout;
using SetsnReps.Domain.Entities.Workout;

namespace SetsnReps.API.Services.Workout;

public class WorkoutRoutineService
{
    private readonly AppDbContext _dbContext;
    
    public WorkoutRoutineService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<WorkoutRoutineResponse>> GetAllWorkoutRoutinesAsync()
    {
        var response = await _dbContext.WorkoutRoutines
            .Include(e => e.Exercises)
            .ThenInclude(es => es.ExerciseSets)
            .Select(wr => wr.ToWorkoutRoutineResponse())
            .AsNoTracking()
            .ToListAsync();

        return response;
    }
    
    /// <summary>
    /// Gets Workout Routine by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns><see cref="WorkoutRoutineResponse"/></returns>
    public async Task<WorkoutRoutineResponse> GetWorkoutRoutineAsync(Guid id)
    {
        var workoutRoutine = await GetWorkoutRoutineByIdAsync(id);
        
        var response = workoutRoutine?.ToWorkoutRoutineResponse();
        return response;
    }
    
    private async Task<WorkoutRoutine> GetWorkoutRoutineByIdAsync(Guid id)
    {
        if(id == Guid.Empty)
            return null;
        
        var workoutRoutine = await _dbContext.WorkoutRoutines
            .Include(wr => wr.Exercises)
            .ThenInclude(we => we.ExerciseSets)
            .FirstOrDefaultAsync(wr => wr.Id == id);
        
        return workoutRoutine;
    }
    
    /// <summary>
    /// add a workout routine
    /// </summary>
    /// <param name="request">The request</param>
    /// <returns><see cref="WorkoutRoutineResponse"/></returns>
    public async Task<WorkoutRoutineResponse> AddWorkoutRoutineAsync(WorkoutRoutineRequest request)
    {
        // TODO: Validation: Must have a name and at least one workout exercise with at least one set.
        var workoutRoutine = request.ToWorkoutRoutine();

        _dbContext.WorkoutRoutines.Add(workoutRoutine);
        await _dbContext.SaveChangesAsync();

        return workoutRoutine.ToWorkoutRoutineResponse();
    }
    
    /// <summary>
    /// Update a workout routine
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"><see cref="WorkoutRoutineRequest"/></param>
    /// <returns></returns>
    public async Task<bool> UpdateWorkoutRoutineAsync(Guid id, WorkoutRoutineRequest request)
    {
        if (request == null)
            return false;
        
        var workoutRoutine = await GetWorkoutRoutineByIdAsync(id);

        if (workoutRoutine == null)
        {
            return false;
        }

        workoutRoutine.Name = request.Name;
        workoutRoutine.Exercises = request.Exercises.ToWorkoutExercises();

        _dbContext.WorkoutRoutines.Update(workoutRoutine);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Delete a workout routine
    /// </summary>
    /// <param name="id"></param>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var workoutRoutine = await GetWorkoutRoutineByIdAsync(id);
        if (workoutRoutine == null)
            return false;

        _dbContext.WorkoutRoutines.Remove(workoutRoutine);
        await _dbContext.SaveChangesAsync();

        return true;
    }

}