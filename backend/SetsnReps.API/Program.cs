
using Microsoft.EntityFrameworkCore;
using SetsnReps.API.Contract.Services.Services.Workout;
using SetsnReps.API.Endpoints.Auth;
using SetsnReps.API.Endpoints.Exercise;
using SetsnReps.API.Endpoints.Workout;
using SetsnReps.API.Infrastructure;
using SetsnReps.API.Middlewares;
using SetsnReps.API.Services.Exercise;
using SetsnReps.API.Services.Identity;
using SetsnReps.API.Services.Workout;

namespace SetsnReps.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddJwtBearerAuthentication();
        
        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddSwaggerService();

        builder.Services.AddDbContext<AppDbContext>(options => 
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                .LogTo(Console.WriteLine, LogLevel.Information));

        builder.Services.AddSingleton<JwtService>();
        builder.Services.AddScoped<ExerciseService>();
        builder.Services.AddScoped<IWorkoutRoutineSetService, WorkoutRoutineSetService>();
        builder.Services.AddScoped<IWorkoutRoutineService, WorkoutRoutineService>();

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

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseAuthEndpoints();
        app.UseExerciseEndpoints();
        app.UseWorkoutRoutineSetEndpoints();
        app.UseWorkoutRoutineEndpoints();
        
        app.Run();
    }
}
