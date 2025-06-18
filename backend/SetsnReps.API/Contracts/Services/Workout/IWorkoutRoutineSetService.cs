using SetsnReps.Core.DTOs.Workout;
using SetsnReps.Core.DTOs.Workout.Base;

namespace SetsnReps.API.Contract.Services.Services.Workout;

public interface IWorkoutRoutineSetService
{
    Task<IEnumerable<SimpleDtoResponse>> GetAllAsync();
    Task<WorkoutRoutineSetResponse?> GetByIdAsync(Guid id);
    Task<WorkoutRoutineSetResponse> AddAsync(string name);
    Task<bool> UpdateAsync(WorkoutRoutineSetRequest request);
    Task<bool> DeleteAsync(Guid id);
}