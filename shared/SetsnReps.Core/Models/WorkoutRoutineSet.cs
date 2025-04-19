namespace SetsnReps.Core.Models;

/// <summary>
/// A set (or collection) of workout routines.
/// </summary>
public class WorkoutRoutineSet
{
    /// <summary>
    /// Workout routine set identifier.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The name of the workout routine set.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// A list of workout routines that are part of the workout routine set.
    /// </summary>
    public IEnumerable<WorkoutRoutine> WorkoutRoutines { get; set; } = new List<WorkoutRoutine>();
    
}
