namespace SetsnReps.Core.Models.Workout;

/// <summary>
/// Workout routine is a set of exercises that are performed together. <br/>
/// E.g.: A Train - Chest and Triceps, B Train - Legs, C Train - Back and Biceps...
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
    public IEnumerable<WorkoutExercise> Exercises { get; set; } = new List<WorkoutExercise>();
    
    public WorkoutRoutineSet WorkoutRoutineSet { get; set; }
    
    public Guid WorkoutRoutineSetId { get; set; }
    
}