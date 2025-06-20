namespace SetsnReps.Core.DTOs.Workout;

public abstract class WorkoutExercise
{
    /// <summary>
    /// A selected exercise id that is part of the RoutineExercise.
    /// </summary>
    public int ExerciseId { get; set; } 
    
    /// <summary>
    /// Notes regarding the exercise execution. How it should be done, what to pay attention to, etc.
    /// Can only be set when the routine is created. 
    /// </summary>
    public string? Notes { get; set; }
    
    /// <summary>
    /// The minimum interval of time to wait between Exercise sets
    /// </summary>
    public int? RestTime { get; set; }
}

public class AddWorkoutExercise : WorkoutExercise
{
    public ICollection<AddExerciseSet> ExerciseSets { get; set; } = new List<AddExerciseSet>();
}

public class UpdateWorkoutExercise : WorkoutExercise
{
    /// <summary>
    /// The identifier of the workout exercise.
    /// </summary>
    public Guid Id { get; set; }
    
    public ICollection<UpdateExerciseSet> ExerciseSets { get; set; } = new List<UpdateExerciseSet>();
}