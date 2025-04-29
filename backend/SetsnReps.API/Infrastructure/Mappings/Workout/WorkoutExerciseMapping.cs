using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetsnReps.Core.Models.Workout;

namespace SetsnReps.API.Infrastructure.Mappings.Workout;

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

        builder.Property(x => x.ExerciseId);

        builder.HasMany(x => x.ExerciseSets)
            .WithOne()
            .HasForeignKey(x=>x.Id);
    }
}