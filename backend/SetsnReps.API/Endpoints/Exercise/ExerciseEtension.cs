namespace SetsnReps.API.Endpoints.Exercise;

public static class ExerciseEtension
{
    /*
    public static WebApplication UseExerciseEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/exercise").WithTags("Exercise");
        
        group.MapGet("/exercise", async (IExerciseRepository repository) =>
        {
            var exercises = await repository.GetAllAsync();
            return Results.Ok(exercises);
        })
        .WithName("GetAllExercises")
        .Produces<IEnumerable<ExerciseDto>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status500InternalServerError);

        group.MapGet("/exercise/{id}", async (IExerciseRepository repository, int id) =>
        {
            var exercise = await repository.GetByIdAsync(id);
            return exercise is not null ? Results.Ok(exercise) : Results.NotFound();
        })
        .WithName("GetExerciseById")
        .Produces<ExerciseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);

        return app;
    }*/
}