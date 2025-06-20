using Microsoft.EntityFrameworkCore;
using SetsnReps.API.Contract.Services.Services.Workout;
using SetsnReps.API.Infrastructure;
using SetsnReps.API.Mappings.Workout;
using SetsnReps.Core.DTOs.Workout;
using SetsnReps.Core.DTOs.Workout.Base;
using SetsnReps.Domain.Entities.Workout;

namespace SetsnReps.API.Services.Workout;

public class WorkoutRoutineService : IWorkoutRoutineService
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<WorkoutRoutineService> _logger;

    
    public WorkoutRoutineService(AppDbContext dbContext, ILogger<WorkoutRoutineService> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<SimpleDtoResponse>> GetAllWorkoutRoutinesAsync()
    {
        IEnumerable<SimpleDtoResponse> response;
        
        try
        {
            var allroutines = await _dbContext.WorkoutRoutines
                .AsNoTracking()
                .Select(wr => wr.ToWorkoutRoutineResponse())
                .ToListAsync();

            response = allroutines.Select(wr => new SimpleDtoResponse()
            {
                Id = wr.Id,
                Name = wr.Name
            });
        
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
        
        return response;
    }
    
    /// <summary>
    /// Gets Workout Routine by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns><see cref="WorkoutRoutineResponse"/></returns>
    public async Task<WorkoutRoutineResponse> GetWorkoutRoutineAsync(Guid id)
    {
        if (id == Guid.Empty)
            return null;

        try
        {
            var workoutRoutine = await GetWorkoutRoutineByIdAsync(id);
        
            var response = workoutRoutine?.ToWorkoutRoutineResponse();
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;       
        }
        
    }
    
    private async Task<WorkoutRoutine> GetWorkoutRoutineByIdAsync(Guid id)
    {
        if(id == Guid.Empty)
            return null;
        
        try
        {
            var workoutRoutine = await _dbContext.WorkoutRoutines
                .Include(wr => wr.Exercises)
                .ThenInclude(we => we.ExerciseSets)
                .FirstOrDefaultAsync(wr => wr.Id == id);
        
            return workoutRoutine;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;       
        }
        
    }
    
    /// <summary>
    /// add a workout routine
    /// </summary>
    /// <param name="request">The request</param>
    /// <returns><see cref="WorkoutRoutineResponse"/></returns>
    public async Task<WorkoutRoutineResponse> AddWorkoutRoutineAsync(AddWorkoutRoutineRequest request)
    {
        if (request == null)
            return null;

        if (string.IsNullOrWhiteSpace(request.Name))
            return null;

        if (!request.Exercises?.Any() ?? true)
            return null;

        try
        {
            var workoutRoutine = request.ToWorkoutRoutine();
            await _dbContext.WorkoutRoutines.AddAsync(workoutRoutine);
            await _dbContext.SaveChangesAsync();
            return workoutRoutine.ToWorkoutRoutineResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao adicionar rotina de exercícios");
            return null;
        }
    }

    
    /// <summary>
    /// Update a workout routine
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"><see cref="WorkoutRoutineRequest"/></param>
    /// <returns></returns>
    public async Task<bool> UpdateWorkoutRoutineAsync(Guid id, UpdateWorkoutRoutineRequest request)
    {
        if (request == null)
            return false;

        try
        {
            var workoutRoutine = await GetWorkoutRoutineByIdAsync(id);

            if (workoutRoutine == null)
            {
                return false;
            }

            workoutRoutine.Name = request.Name;
            workoutRoutine.Exercises = request.Exercises.ToWorkoutExercises();

            _dbContext.WorkoutRoutines.Update(workoutRoutine);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return false;
        }

        return true;
    }

    /// <summary>
    /// Delete a workout routine
    /// </summary>
    /// <param name="id"></param>
    public async Task<bool> DeleteAsync(Guid id)
    {
        if (id == Guid.Empty)
            return false;

        try
        {
            var workoutRoutine = await GetWorkoutRoutineByIdAsync(id);
            if (workoutRoutine == null)
                return false;

            _dbContext.WorkoutRoutines.Remove(workoutRoutine);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return false;
        }
        

        return true;
    }

}