
using Entity = SetsnReps.Domain.Entities.Workout;
using Dto = SetsnReps.Core.DTOs.Workout;

namespace SetsnReps.API.Mappings.Workout;

public static class WorkoutRoutineExtensions
{
    public static Dto.WorkoutRoutineResponse ToWorkoutRoutineResponse(this Entity.WorkoutRoutine workoutRoutine)
    {
        var response = new Dto.WorkoutRoutineResponse()
        {
            Id = workoutRoutine.Id,
            Name = workoutRoutine.Name,
            WorkoutRoutineSetId = workoutRoutine.WorkoutRoutineSetId,
            Exercises = workoutRoutine.Exercises.Select(x => new Dto.UpdateWorkoutExercise
            {
                Id = x.Id,
                ExerciseId = x.ExerciseId,
                ExerciseSets = x.ExerciseSets.Select(y => new Dto.UpdateExerciseSet()
                {
                    Id = y.Id,
                    Reps = y.Reps,
                    Weight = y.Weight,
                    Duration = y.Duration,
                    OrderNumber = y.OrderNumber
                }).ToList()
            }).ToList()
        };
        
        return response;
    }
    
    public static Entity.WorkoutRoutine ToWorkoutRoutine(this Dto.AddWorkoutRoutineRequest request)
    {
        var workoutRoutine = new Entity.WorkoutRoutine
        {
            Name = request.Name,
            WorkoutRoutineSetId = request.WorkoutRoutineSetId,
            Exercises = request.Exercises.ToWorkoutExercises()
        };
        
        return workoutRoutine;
    }
    
    public static Entity.WorkoutRoutine ToWorkoutRoutine(this Dto.UpdateWorkoutRoutineRequest request, Guid id)
    {
        var workoutRoutine = new Entity.WorkoutRoutine
        {
            Name = request.Name,
            Id = id,
            WorkoutRoutineSetId = request.WorkoutRoutineSetId,
            Exercises = request.Exercises.ToWorkoutExercises()
        };
        
        return workoutRoutine;
    }
}