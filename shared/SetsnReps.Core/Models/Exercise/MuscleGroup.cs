namespace SetsnReps.Core.Models.Exercise;

/// <summary>
/// The muscle group that is targeted by an exercise.
/// </summary>
public class MuscleGroup
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
}