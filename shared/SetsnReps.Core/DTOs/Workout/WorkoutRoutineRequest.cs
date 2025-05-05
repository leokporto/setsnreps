using SetsnReps.Core.DTOs.Workout.Base;

namespace SetsnReps.Core.DTOs.Workout;

public class WorkoutRoutineRequest : SimpleDto
{
    public ICollection<WorkoutExercise> Exercises { get; set; } = new List<WorkoutExercise>();
}