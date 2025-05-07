using Microsoft.AspNetCore.Http.HttpResults;
using SetsnReps.API.Services.Workout;
using SetsnReps.Core.DTOs.Workout;
using SetsnReps.Core.DTOs.Workout.Base;

namespace SetsnReps.API.Endpoints.Workout;

public static class WorkoutRoutineExtensions
{
    public static WebApplication UseWorkoutRoutineEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/workout-routines")
            .WithTags("Workout Routines")
            .WithOpenApi();

        group.MapGet("/", async Task<Results<Ok<IEnumerable<SimpleDtoResponse>>, NotFound>>
            (WorkoutRoutineService workoutRoutineService) =>
            {
                var result = await workoutRoutineService.GetAllWorkoutRoutinesAsync();
                if (result is null)
                    return TypedResults.NotFound();
                
                return TypedResults.Ok(result);
            })
            .WithName("GetAllWorkoutRoutines");
        
        group.MapGet("/{id:guid}", async Task<Results<Ok<WorkoutRoutineResponse>, NotFound>>
            (Guid id, WorkoutRoutineService workoutRoutineService) =>
            {
                var result = await workoutRoutineService.GetWorkoutRoutineAsync(id);
                if (result is null)
                    return TypedResults.NotFound();
                
                return TypedResults.Ok(result);
            })
            .WithName("GetWorkoutRoutineById");
        
        group.MapPost("/", async Task<Results<Created<WorkoutRoutineResponse>, BadRequest>>
            (WorkoutRoutineRequest workoutRoutineRequest, WorkoutRoutineService workoutRoutineService) =>
            {
                var result = await workoutRoutineService.AddWorkoutRoutineAsync(workoutRoutineRequest);
                if (result is null)
                    return TypedResults.BadRequest();
                
                return TypedResults.Created($"/workout-routines/{result.Id}", result);
            })
            .WithName("AddWorkoutRoutine");
        
        group.MapPut("/{id:guid}", async Task<Results<Ok<bool>, NotFound>>
            (Guid id, WorkoutRoutineRequest workoutRoutineRequest, WorkoutRoutineService workoutRoutineService) =>
            {
                var result = await workoutRoutineService.UpdateWorkoutRoutineAsync(id, workoutRoutineRequest);
                if (!result)
                    return TypedResults.NotFound();
                
                return TypedResults.Ok(result);
            })
            .WithName("UpdateWorkoutRoutine");
        
        group.MapDelete("/{id:guid}", async Task<Results<Ok<bool>, NotFound>>
            (Guid id, WorkoutRoutineService workoutRoutineService) =>
            {
                var result = await workoutRoutineService.DeleteAsync(id);
                if (!result)
                    return TypedResults.NotFound();
                
                return TypedResults.Ok(result);
            })
            .WithName("DeleteWorkoutRoutine");

        return app;
    }
        
    
}