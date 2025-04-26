using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetsnReps.Core.Models.Workout;

namespace SetsnReps.API.Infrastructure.Mappings.Workout;

public class ExerciseSetMapping : IEntityTypeConfiguration<ExerciseSet>
{
    public void Configure(EntityTypeBuilder<ExerciseSet> builder)
    {
        builder.ToTable("ExerciseSets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrderNumber)
            .IsRequired(true);

        builder.Property(x => x.Reps)
            .IsRequired(false);

        builder.Property(x => x.Weight)
            .IsRequired(false);

        builder.Property(x => x.Duration)
            .IsRequired(false);
    }
}

public class WorkoutExerciseMapping : IEntityTypeConfiguration<WorkoutExercise>
{
    public void Configure(EntityTypeBuilder<WorkoutExercise> builder)
    {
        builder.ToTable("WorkoutExercises");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Notes)
            .HasMaxLength(500)
            .IsRequired(false);
        
        builder.Property(x => x.RestTime)
            .IsRequired(false);

        builder.HasOne(x => x.SelectedExercise)
            .WithMany();

        builder.HasMany(x => x.ExerciseSets)
            .WithOne()
            .HasForeignKey(x => x.Id);
    }
}

public class WorkoutRoutineMapping : IEntityTypeConfiguration<WorkoutRoutine>
{
    public void Configure(EntityTypeBuilder<WorkoutRoutine> builder)
    {
        builder.ToTable("WorkoutRoutines");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired(true);

        builder.HasMany(x => x.Exercises)
            .WithOne()
            .HasForeignKey(x => x.Id);
    }
}

public class WorkoutRoutineSetMapping : IEntityTypeConfiguration<WorkoutRoutineSet>
{
    public void Configure(EntityTypeBuilder<WorkoutRoutineSet> builder)
    {
        builder.ToTable("WorkoutRoutineSets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired(true);

        builder.HasMany(x => x.WorkoutRoutines)
            .WithOne()
            .HasForeignKey(x => x.Id);

    }
}

