namespace SetsnReps.Core.DTOs.Workout.Base;

public class SimpleDto
{
    /// <summary>
    /// Exercise identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the exercise.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}