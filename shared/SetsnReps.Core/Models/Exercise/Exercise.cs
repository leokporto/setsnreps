using SetsnReps.Core.Enums;

namespace SetsnReps.Core.Models.Exercise;

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
    
    public EquipmentType EquipmentType { get; set; }
    
    public Guid EquipmentTypeId { get; set; }
    
    public MuscleGroup PrimaryMuscleGroup { get; set; }
    
    public Guid PrimaryMuscleGroupId { get; set; }
}