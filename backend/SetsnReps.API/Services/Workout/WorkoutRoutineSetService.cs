using Microsoft.EntityFrameworkCore;
using SetsnReps.API.Contract.Services.Services.Workout;
using SetsnReps.API.Infrastructure;
using SetsnReps.API.Mappings.Workout;
using SetsnReps.Core.DTOs.Workout;
using SetsnReps.Core.DTOs.Workout.Base;
using SetsnReps.Domain.Entities.Workout;

namespace SetsnReps.API.Services.Workout;



public class WorkoutRoutineSetService : IWorkoutRoutineSetService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<WorkoutRoutineSetService> _logger;

    
    public WorkoutRoutineSetService(AppDbContext dbContext, 
        ILogger<WorkoutRoutineSetService> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

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
        if (id == Guid.Empty)
            return null;

        try
        {
            var response = await _dbContext.WorkoutRoutineSets
                .AsNoTracking()
                .Where(wrs => wrs.Id == id)
                .Select(wrs => wrs.ToWorkoutRoutineSetResponse())
                .FirstOrDefaultAsync();

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar workout routine set: {Id}", id);
            throw;
        }

    }

    public async Task<WorkoutRoutineSetResponse> AddAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        try
        {
            var workoutRoutineSet = new WorkoutRoutineSet
            {
                Name = name.Trim()
            };

            await _dbContext.WorkoutRoutineSets.AddAsync(workoutRoutineSet);
            await _dbContext.SaveChangesAsync();

            return workoutRoutineSet.ToWorkoutRoutineSetResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao adicionar workout routine set: {Name}", name);
            throw;
        }

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