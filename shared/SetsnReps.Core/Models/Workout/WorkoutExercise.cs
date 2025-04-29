namespace SetsnReps.Core.Models.Workout;

/// <summary>
/// A list of exercise sets that are performed together.
/// </summary>
public class WorkoutExercise
{
    public Guid Id { get; set; }

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
    
    public Guid WorkoutRoutineId { get; set; }
    public WorkoutRoutine WorkoutRoutine { get; set; } = null!;
    
    public ICollection<ExerciseSet> ExerciseSets { get; set; } = new List<ExerciseSet>();

}