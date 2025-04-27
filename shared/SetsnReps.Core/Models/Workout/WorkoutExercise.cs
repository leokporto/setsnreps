namespace SetsnReps.Core.Models.Workout;

/// <summary>
/// A list of exercise sets that are performed together.
/// </summary>
public class WorkoutExercise
{
    public Guid Id { get; set; }

    /// <summary>
    /// A selected exercise that is part of the RoutineExercise.
    /// </summary>
    public Exercise.Exercise SelectedExercise { get; set; } = null!;
    
    /// <summary>
    /// Notes regarding the exercise execution. How it should be done, what to pay attention to, etc.
    /// Can only be set when the routine is created. 
    /// </summary>
    public string? Notes { get; set; }
    
    /// <summary>
    /// The minimum interval of time to wait between Exercise sets
    /// </summary>
    public int? RestTime { get; set; }
    
    public IEnumerable<ExerciseSet> ExerciseSets { get; set; } = new List<ExerciseSet>();

    public WorkoutRoutine WorkoutRoutine { get; set; } = null!;
    
    public Guid WorkoutRoutineId { get; set; }
}