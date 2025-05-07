using Dto = SetsnReps.Core.DTOs.Workout;
using Entity = SetsnReps.Domain.Entities.Workout;

namespace SetsnReps.API.Mappings.Workout;

public static class WorkoutExerciseExtensions
{
    public static ICollection<Entity.WorkoutExercise> ToWorkoutExercises(
        this ICollection<Dto.WorkoutExercise> workoutRoutineSets)
    {
        var converted = workoutRoutineSets.Select(x => new Entity.WorkoutExercise
        {
            ExerciseId = x.ExerciseId,
            ExerciseSets = x.ExerciseSets.Select(y => new Entity.ExerciseSet()
            {
                Reps = y.Reps,
                Weight = y.Weight,
                Duration = y.Duration,
                OrderNumber = y.OrderNumber

            }).ToList()
        }).ToList();

        return converted;
    }
}