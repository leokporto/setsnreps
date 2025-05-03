using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetsnReps.Domain.Entities.Workout;

namespace SetsnReps.API.Infrastructure.Mappings.Workout;

public class WorkoutExerciseMapping : IEntityTypeConfiguration<WorkoutExercise>
{
    public void Configure(EntityTypeBuilder<WorkoutExercise> builder)
    {
        builder.ToTable("WorkoutExercises");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.Notes)
            .HasMaxLength(500)
            .IsRequired(false);
        
        builder.Property(x => x.RestTime)
            .IsRequired(false);
        
        builder.Property(we => we.ExerciseId)
            .IsRequired();
        
        builder.Property(we => we.WorkoutRoutineId)
            .IsRequired();
        builder.HasIndex(we => we.WorkoutRoutineId);

        builder.HasOne(we => we.WorkoutRoutine)
            .WithMany(wr => wr.Exercises)
            .HasForeignKey(we => we.WorkoutRoutineId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}