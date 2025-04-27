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
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

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