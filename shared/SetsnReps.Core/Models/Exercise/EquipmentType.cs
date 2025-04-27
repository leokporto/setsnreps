namespace SetsnReps.Core.Models.Exercise;

public class EquipmentType
{
    /// <summary>
    /// Exercise identifier.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The name of the exercise.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}