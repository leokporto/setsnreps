using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetsnReps.Core.Models.Workout;

namespace SetsnReps.API.Infrastructure.Mappings.Workout;

public class WorkoutRoutineSetMapping : IEntityTypeConfiguration<WorkoutRoutineSet>
{
    public void Configure(EntityTypeBuilder<WorkoutRoutineSet> builder)
    {
        builder.ToTable("WorkoutRoutineSets");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired(true);

        builder.HasMany(x => x.WorkoutRoutines)
            .WithOne()
            .HasForeignKey(x => x.Id);

    }
}