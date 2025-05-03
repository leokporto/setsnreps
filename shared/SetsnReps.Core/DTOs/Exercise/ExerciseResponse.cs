namespace SetsnReps.Core.DTOs.Exercise;

public class ExerciseResponse
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

    /// <summary>
    /// The name of the equipment type used for the exercise.
    /// </summary>
    public string EquipmentType { get; set; } = string.Empty;
    
    public int EquipmentTypeId { get; set; }
    
    /// <summary>
    /// The name of the primary muscle group targeted by the exercise.
    /// </summary>
    public string MuscleGroup { get; set; } = string.Empty;
    
    public int MuscleGroupId { get; set; }
}