namespace SetsnReps.Core.DTOs.Workout;

public class AddWorkoutRoutineRequest
{
    /// <summary>
    /// The name of the exercise.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    public ICollection<AddWorkoutExercise> Exercises { get; set; } = new List<AddWorkoutExercise>();
}