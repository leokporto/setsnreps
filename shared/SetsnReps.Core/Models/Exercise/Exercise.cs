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
    public int PrimaryMuscleGroupId { get; set; }
    public EquipmentType EquipmentType { get; set; } = null!;
    public MuscleGroup PrimaryMuscleGroup { get; set; } = null!;
    
    
}