using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetsnReps.Domain.Entities.Training;

namespace SetsnReps.API.Infrastructure.Mappings.Training;

public class ExecutedWorkoutMapping : IEntityTypeConfiguration<ExecutedWorkout>
{
   
    public void Configure(EntityTypeBuilder<ExecutedWorkout> builder)
    {
        builder.ToTable("ExecutedWorkouts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.WorkoutRoutineId).IsRequired();
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Duration).IsRequired();
        builder.Property(x => x.WeightVolume).IsRequired();

        builder.HasOne(x => x.WorkoutRoutine)
            .WithMany()
            .HasForeignKey(x => x.WorkoutRoutineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.WorkoutRoutineId);
    }
    
}