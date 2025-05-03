namespace SetsnReps.Domain.Entities.Training;

public class ExecutedWorkout
{
    public Guid Id { get; set; }

    public Workout.WorkoutRoutine WorkoutRoutine { get; set; } = new();
    
    public Guid WorkoutRoutineId { get; set; }
    
    public DateTimeOffset Date { get; set; }
    
    public TimeSpan Duration { get; set; }
    
    public int WeightVolume { get; set; }
}