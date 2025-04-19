namespace SetsnReps.Core.Models;

/// <summary>
/// Workout routine is a set of exercises that are performed together.
/// </summary>
public class WorkoutRoutine
{
    /// <summary>
    /// Workout routine identifier.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The name of the workout routine.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// A list of exercises that are part of the workout routine.
    /// </summary>
    public IEnumerable<ExerciseSet> Exercises { get; set; } = new List<ExerciseSet>();
    
}