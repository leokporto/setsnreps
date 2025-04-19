namespace SetsnReps.Core.Models;

/// <summary>
/// The muscle group that is targeted by an exercise.
/// </summary>
public class MuscleGroup
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
}