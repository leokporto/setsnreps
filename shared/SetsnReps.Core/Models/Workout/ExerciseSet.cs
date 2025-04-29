namespace SetsnReps.Core.Models.Workout;

/// <summary>
/// The representation of a single execution on a set. <br/>
/// E.g.: The first execution will be 12 reps with 20 kg.
/// </summary>
public class ExerciseSet
{
    public Guid Id { get; set; }
    
    /// <summary>
    /// The order of the set execution item.
    /// </summary>
    public int OrderNumber { get; set; }
    
    /// <summary>
    /// Rep duration in seconds (if not weight execution).
    /// </summary>
    public int? Duration { get; set; }
    
    /// <summary>
    /// The number of repetitions for the set execution item (if not duration execution).
    /// </summary>
    public int? Reps { get; set; }
    
    /// <summary>
    /// The weight used for the set execution item (if not DurationExecution).
    /// </summary>
    public Single? Weight { get; set; }
    
    /// <summary>
    /// Fk to the exercise set.
    /// </summary>
    public Guid WorkoutExerciseId { get; set; }

    /// <summary>
    /// The parent workout exercise.
    /// </summary>
    public WorkoutExercise WorkoutExercise { get; set; } = null!;

}