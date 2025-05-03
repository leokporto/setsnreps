

namespace SetsnReps.Domain.Entities.Exercise;

/// <summary>
/// Representation of an exercise.
/// </summary>
public class Exercise
{
    /// <summary>
    /// Exercise identifier.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The name of the exercise.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// The thumbnail image URI for the exercise.
    /// </summary>
    public string ThumbnailUri { get; set; } = string.Empty;

    public int EquipmentTypeId { get; set; }
    public int MuscleGroupId { get; set; }
    public EquipmentType EquipmentType { get; set; } = null!;
    public MuscleGroup MuscleGroup { get; set; } = null!;
    
    
}