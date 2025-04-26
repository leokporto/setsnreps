using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SetsnReps.Core.Models.Exercise;
using SetsnReps.Core.Models.Training;
using SetsnReps.Core.Models.Workout;

namespace SetsnReps.API.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<MuscleGroup> MuscleGroups { get; set; } = null!;
    
    public DbSet<EquipmentType> EquipmentTypes { get; set; } = null!;
    
    public DbSet<Exercise> Exercises { get; set; } = null!;
    
    public DbSet<WorkoutRoutineSet> WorkoutRoutineSets { get; set; } = null!;
    
    public DbSet<WorkoutRoutine> WorkoutRoutines { get; set; } = null!;
    
    public DbSet<WorkoutExercise> WorkoutExercises { get; set; } = null!;
    
    public DbSet<ExerciseSet> ExerciseSets { get; set; } = null!;
    
    public DbSet<ExecutedWorkout> ExecutedWorkouts { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}