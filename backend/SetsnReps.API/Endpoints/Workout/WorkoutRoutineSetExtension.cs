using Microsoft.AspNetCore.Http.HttpResults;
using SetsnReps.API.Services.Workout;
using SetsnReps.Core.DTOs.Workout;
using SetsnReps.Core.DTOs.Workout.Base;
using SetsnReps.Domain.Entities.Workout;

namespace SetsnReps.API.Endpoints.Workout;

public static class WorkoutRoutineSetExtension
{
    public static WebApplication UseWorkoutRoutineSetEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("workout-routine-sets")
            .WithTags("WorkoutRoutineSet")
            .WithOpenApi();
            
        group.MapGet("/", async Task<Results<Ok<IEnumerable<SimpleDtoResponse>>, NotFound>>
            (WorkoutRoutineSetService svc) =>
            {
                var workoutRoutineSets = await svc.GetAllAsync();
                return workoutRoutineSets is not null 
                    ? TypedResults.Ok(workoutRoutineSets)
                    : TypedResults.NotFound();
            })
            .WithName("GetAllWorkoutRoutineSets");
            
        group.MapGet("/{id}", async Task<Results<Ok<WorkoutRoutineSetResponse>, NotFound>>
            (WorkoutRoutineSetService svc, Guid id) =>
            {
                if (id == Guid.Empty)
                    return TypedResults.NotFound();
                
                var workoutRoutineSet = await svc.GetByIdAsync(id);
                return workoutRoutineSet is not null 
                    ? TypedResults.Ok(workoutRoutineSet) 
                    : TypedResults.NotFound();
            })
            .WithName("GetWorkoutRoutineSetById");
            
        group.MapPost("/", async Task<Results<Created<WorkoutRoutineSetResponse>, BadRequest>>
            (WorkoutRoutineSetService svc, string name) =>
            {
                if (string.IsNullOrWhiteSpace(name))
                    return TypedResults.BadRequest();
                    
                var response = await svc.AddAsync(name);
                return TypedResults.Created($"/workout-routine-sets/{response.Id}", response);
            })
            .WithName("AddWorkoutRoutineSet");
            
        group.MapPut("/{id}", async Task<Results<NoContent, BadRequest, NotFound>>
            (WorkoutRoutineSetService svc, WorkoutRoutineSetRequest request) =>
            {
                if (request is null || request.Id == Guid.Empty || string.IsNullOrWhiteSpace(request.Name))
                    return TypedResults.BadRequest();

                var exists = await svc.GetByIdAsync(request.Id);
                if (exists is null)
                    return TypedResults.NotFound();

                bool response = await svc.UpdateAsync(request);
                if (!response)
                    return TypedResults.BadRequest();
                
                return TypedResults.NoContent();
            });
            
        group.MapDelete("/{id}", async Task<Results<NoContent, NotFound>>
            (WorkoutRoutineSetService svc, Guid id) =>
            {
                var exists = await svc.GetByIdAsync(id);
                if (exists is null)
                    return TypedResults.NotFound();

                bool response = await svc.DeleteAsync(id);
                
                if (!response)
                    return TypedResults.NotFound();
                
                return TypedResults.NoContent();
            });
        
        return app;
    }
}