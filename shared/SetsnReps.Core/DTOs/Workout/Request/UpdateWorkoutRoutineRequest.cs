using SetsnReps.Core.DTOs.Workout.Base;

namespace SetsnReps.Core.DTOs.Workout;

public class UpdateWorkoutRoutineRequest
{
    public Guid Id { get; set; }
    
    /// <summary>
    /// The name of the exercise.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    public Guid WorkoutRoutineSetId { get; set; }
    
    public ICollection<UpdateWorkoutExercise> Exercises { get; set; } = new List<UpdateWorkoutExercise>();
}