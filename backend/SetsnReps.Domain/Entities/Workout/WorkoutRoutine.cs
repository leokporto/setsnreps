namespace SetsnReps.Domain.Entities.Workout;

/// <summary>
/// Workout routine is a set of exercises that are performed together. <br/>
/// E.g.: A Train - Chest and Triceps, B Train - Legs, C Train - Back and Biceps...
/// </summary>
/// <remarks>Workout routines can be part of a Workout routine set or not.</remarks>
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

    public Guid WorkoutRoutineSetId { get; set; }
    
    public WorkoutRoutineSet? WorkoutRoutineSet { get; set; }
    
    /// <summary>
    /// A list of exercises that are part of the workout routine.
    /// </summary>
    public ICollection<WorkoutExercise> Exercises { get; set; } = new List<WorkoutExercise>();

    
}