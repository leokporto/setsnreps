using SetsnReps.Core.DTOs.Workout;
using SetsnReps.Domain.Entities.Workout;

namespace SetsnReps.API.Mappings.Workout;

public static class WorkoutRoutineSetExtensions
{
    public static WorkoutRoutineSetResponse ToWorkoutRoutineSetResponse(this WorkoutRoutineSet workoutRoutineSet)
    {
        return new WorkoutRoutineSetResponse
        {
            Id = workoutRoutineSet.Id,
            Name = workoutRoutineSet.Name,
        };
    }
}