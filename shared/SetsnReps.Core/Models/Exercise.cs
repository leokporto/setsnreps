using SetsnReps.Core.Enums;

namespace SetsnReps.Core.Models;

/// <summary>
/// Representation of an exercise.
/// </summary>
public class Exercise
{
    /// <summary>
    /// Exercise identifier.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The name of the exercise.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// The thumbnail image URI for the exercise.
    /// </summary>
    public string ThumbnailUri { get; set; } = string.Empty;
    
    public eEquipmentType EquipmentType { get; set; }
    
    public MuscleGroup PrimaryMuscleGroup { get; set; }
}