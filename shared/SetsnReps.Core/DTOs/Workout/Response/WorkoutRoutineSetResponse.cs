using SetsnReps.Core.DTOs.Workout.Base;

namespace SetsnReps.Core.DTOs.Workout;

public class WorkoutRoutineSetResponse : SimpleDtoResponse
{
    public IEnumerable<SimpleDtoResponse>? WorkoutRoutines { get; set; }
}