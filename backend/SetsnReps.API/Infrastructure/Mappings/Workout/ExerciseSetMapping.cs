using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetsnReps.Domain.Entities.Workout;

namespace SetsnReps.API.Infrastructure.Mappings.Workout;

public class ExerciseSetMapping : IEntityTypeConfiguration<ExerciseSet>
{
    public void Configure(EntityTypeBuilder<ExerciseSet> builder)
    {
        builder.ToTable("ExerciseSets");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.OrderNumber)
            .IsRequired(true);

        builder.Property(x => x.Reps)
            .IsRequired(false);

        builder.Property(x => x.Weight)
            .HasColumnType("numeric(6, 2)")
            .IsRequired(false);

        builder.Property(x => x.Duration)
            .IsRequired(false);

        builder.Property(es => es.WorkoutExerciseId)
            .IsRequired();
        builder.HasIndex(es => es.WorkoutExerciseId);

        builder.HasOne(es => es.WorkoutExercise)
            .WithMany(we => we.ExerciseSets)
            .HasForeignKey(es => es.WorkoutExerciseId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}