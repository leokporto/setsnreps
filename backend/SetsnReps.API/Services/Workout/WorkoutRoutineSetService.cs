using Microsoft.EntityFrameworkCore;
using SetsnReps.API.Infrastructure;
using SetsnReps.API.Mappings.Workout;
using SetsnReps.Core.DTOs.Workout;
using SetsnReps.Core.DTOs.Workout.Base;
using SetsnReps.Domain.Entities.Workout;

namespace SetsnReps.API.Services.Workout;

public class WorkoutRoutineSetService
{
    private readonly AppDbContext _dbContext;

    public WorkoutRoutineSetService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<SimpleDtoResponse>> GetAllAsync()
    {
        var workoutRoutineSets = await _dbContext.WorkoutRoutineSets
            .AsNoTracking()
            .Select(wrs => wrs.ToWorkoutRoutineSetResponse())
            .ToListAsync();

        return workoutRoutineSets;
    }

    public async Task<WorkoutRoutineSetResponse?> GetByIdAsync(Guid id)
    {
        var response = await _dbContext.WorkoutRoutineSets
            .Include(r => r.WorkoutRoutines)
            .Where(wrs => wrs.Id == id)
            .Select(wrs => wrs.ToWorkoutRoutineSetResponse())
            .FirstOrDefaultAsync();
        
        return response;
    }

    public async Task<WorkoutRoutineSetResponse> AddAsync(string name)
    {
        var workoutRoutineSet = new WorkoutRoutineSet
        {
            Name = name
        };

        await _dbContext.WorkoutRoutineSets.AddAsync(workoutRoutineSet);
        await _dbContext.SaveChangesAsync();
        
        return workoutRoutineSet.ToWorkoutRoutineSetResponse();
    }

    public async Task<bool> UpdateAsync(WorkoutRoutineSetRequest request)
    {
        if (request == null)
            return false;
                
        var workoutRoutineSet = await _dbContext.WorkoutRoutineSets
            .FirstOrDefaultAsync(wrs => wrs.Id == request.Id);

        if (workoutRoutineSet == null)
            return false;

        workoutRoutineSet.Name = request.Name;

        _dbContext.WorkoutRoutineSets.Update(workoutRoutineSet);
        int result = await _dbContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var workoutRoutineSet = await _dbContext.WorkoutRoutineSets
            .FirstOrDefaultAsync(wrs => wrs.Id == id);
        
        int result = 0;
        
        if (workoutRoutineSet != null)
        {
            _dbContext.WorkoutRoutineSets.Remove(workoutRoutineSet);
            result = await _dbContext.SaveChangesAsync();
        }
        
        return result > 0;
    }
}