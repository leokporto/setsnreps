
using Microsoft.EntityFrameworkCore;
using SetsnReps.API.Endpoints.Exercise;
using SetsnReps.API.Endpoints.Workout;
using SetsnReps.API.Infrastructure;
using SetsnReps.API.Middlewares;
using SetsnReps.API.Services.Exercise;
using SetsnReps.API.Services.Workout;

namespace SetsnReps.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<ExerciseService>();
        builder.Services.AddScoped<WorkoutRoutineSetService>();
        builder.Services.AddScoped<WorkoutRoutineService>();

        builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
        
        var app = builder.Build();
        
        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseExerciseEndpoints();
        app.UseWorkoutRoutineSetEndpoints();
        

        app.Run();
    }
}
