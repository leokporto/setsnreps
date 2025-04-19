namespace SetsnReps.Core.Models;

/// <summary>
/// The representation of a single execution on a set. E.g.: The first execution will be 12 reps with 20kg.
/// </summary>
public class SetExecutionItem
{
    public Guid SetExecutionItemId { get; set; }
    
    /// <summary>
    /// The order of the set execution item.
    /// </summary>
    public int Number { get; set; }
    
    public Single? Weight { get; set; }
    
    /// <summary>
    /// Rep duration in seconds.
    /// </summary>
    public int? Duration { get; set; }
    
    public int Reps { get; set; }
}