
using SetsnReps.API.Services.Exercise;
using SetsnReps.Core.DTOs.Exercise;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SetsnReps.API.Endpoints.Exercise;

public static class ExerciseExtension
{
    public static WebApplication UseExerciseEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/exercise").WithOpenApi();
        
        group.MapGet("/exercise", async Task<Results<Ok<IEnumerable<ExerciseResponse>>, NotFound>>
            (ExerciseService exerciseSvc) =>
            {

                IEnumerable<ExerciseResponse> exercises = await exerciseSvc.GetAllExercisesAsync();
                
                return exercises is null || !exercises.Any()
                    ? TypedResults.NotFound()
                    : TypedResults.Ok(exercises);
            })
            .WithName("GetAllExercises");
        
        group.MapGet("/exercise/{id:int}", async Task<Results<Ok<ExerciseResponse>, NotFound>>
            (int id, ExerciseService exerciseSvc) =>
            {
                if (id <= 0)
                    return TypedResults.NotFound();
                
                var exercise = await exerciseSvc.GetExerciseByIdAsync(id);
                return exercise is null 
                    ? TypedResults.NotFound() 
                    : TypedResults.Ok(exercise);
            })
            .WithName("GetExerciseById");
        
        group.MapGet("/exercise/name/{name}", async Task<Results<Ok<IEnumerable<ExerciseResponse>>, NotFound>>
            (string name, ExerciseService exerciseSvc) =>
            {
                if (string.IsNullOrWhiteSpace(name))
                    return TypedResults.NotFound();
                
                var exercises = await exerciseSvc.GetExercisesByNameAsync(name);
                return exercises is null || !exercises.Any()
                    ? TypedResults.NotFound()
                    : TypedResults.Ok(exercises);
            })
            .WithName("GetExercisesByName");
        
        group.MapGet("/exercise/equipment/{equipmentTypeId:int}", 
            async Task<Results<Ok<IEnumerable<ExerciseResponse>>, NotFound>>
            (int equipmentTypeId, ExerciseService exerciseSvc) =>
            {
                if (equipmentTypeId <= 0)
                    return TypedResults.NotFound();
                
                var exercises = await exerciseSvc.GetExercisesByEquipmentTypeIdAsync(equipmentTypeId);
                return exercises is null || !exercises.Any()
                    ? TypedResults.NotFound()
                    : TypedResults.Ok(exercises);
            })
            .WithName("GetExercisesByEquipmentTypeId");
        
        group.MapGet("/exercise/muscle/{muscleGroupId:int}", 
            async Task<Results<Ok<IEnumerable<ExerciseResponse>>, NotFound>>
            (int muscleGroupId, ExerciseService exerciseSvc) =>
            {
                if (muscleGroupId <= 0)
                    return TypedResults.NotFound();
                
                var exercises = await exerciseSvc.GetExercisesByPrimaryMuscleGroupIdAsync(muscleGroupId);
                return exercises is null || !exercises.Any()
                    ? TypedResults.NotFound()
                    : TypedResults.Ok(exercises);
            })
            .WithName("GetExercisesByMuscleGroupId");
        
        return app;
    }

}