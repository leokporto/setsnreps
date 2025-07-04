using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SetsnReps.API.Entities;
using SetsnReps.API.Infrastructure.Mappings;
using SetsnReps.API.Infrastructure.Mappings.Workout;
using SetsnReps.Domain.Entities.Exercise;
using SetsnReps.Domain.Entities.Training;
using SetsnReps.Domain.Entities.Workout;

namespace SetsnReps.API.Infrastructure;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid,
    IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
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
        base.OnModelCreating(modelBuilder); // 🔥 ESSENCIAL
        
       // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
       modelBuilder.ApplyConfiguration(new EquipmentTypeMapping());
       modelBuilder.ApplyConfiguration(new MuscleGroupMapping());
       modelBuilder.ApplyConfiguration(new ExerciseMapping());
       modelBuilder.ApplyConfiguration(new WorkoutRoutineSetMapping());
       modelBuilder.ApplyConfiguration(new WorkoutRoutineMapping());
       modelBuilder.ApplyConfiguration(new WorkoutExerciseMapping());
       modelBuilder.ApplyConfiguration(new ExerciseSetMapping());
    }
}