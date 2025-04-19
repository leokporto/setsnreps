namespace SetsnReps.Core.Models;

/// <summary>
/// A set of exercises that are performed together.
/// </summary>
public class ExerciseSet
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string ThumbnailUri { get; set; } = string.Empty;
    
    public string? Notes { get; set; } = string.Empty;
    
    public TimeSpan? RestTime { get; set; }
    
    public IEnumerable<SetExecutionItem> Reps { get; set; } = new List<SetExecutionItem>();
}