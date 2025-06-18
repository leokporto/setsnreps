using SetsnReps.Core.DTOs.Workout;
using SetsnReps.Core.DTOs.Workout.Base;

namespace SetsnReps.API.Contract.Services.Services.Workout;

public interface IWorkoutRoutineService
{
    Task<IEnumerable<SimpleDtoResponse>> GetAllWorkoutRoutinesAsync();

    /// <summary>
    /// Gets Workout Routine by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns><see cref="WorkoutRoutineResponse"/></returns>
    Task<WorkoutRoutineResponse> GetWorkoutRoutineAsync(Guid id);

    /// <summary>
    /// add a workout routine
    /// </summary>
    /// <param name="request">The request</param>
    /// <returns><see cref="WorkoutRoutineResponse"/></returns>
    Task<WorkoutRoutineResponse> AddWorkoutRoutineAsync(WorkoutRoutineRequest request);

    /// <summary>
    /// Update a workout routine
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"><see cref="WorkoutRoutineRequest"/></param>
    /// <returns></returns>
    Task<bool> UpdateWorkoutRoutineAsync(Guid id, WorkoutRoutineRequest request);

    /// <summary>
    /// Delete a workout routine
    /// </summary>
    /// <param name="id"></param>
    Task<bool> DeleteAsync(Guid id);
}