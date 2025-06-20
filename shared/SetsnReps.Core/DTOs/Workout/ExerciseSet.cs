namespace SetsnReps.Core.DTOs.Workout;

public abstract class ExerciseSet
{
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
    public decimal? Weight { get; set; }

}

public class AddExerciseSet : ExerciseSet
{
    
}

public class UpdateExerciseSet : ExerciseSet
{
    /// <summary>
    /// The identifier of the exercise set.
    /// </summary>
    public Guid Id { get; set; }
}