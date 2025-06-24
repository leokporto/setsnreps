using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetsnReps.Domain.Entities.Workout;

namespace SetsnReps.API.Infrastructure.Mappings.Workout;

public class WorkoutRoutineMapping : IEntityTypeConfiguration<WorkoutRoutine>
{
    public void Configure(EntityTypeBuilder<WorkoutRoutine> builder)
    {
        builder.ToTable("WorkoutRoutines");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired(true);

        builder.Property(wr => wr.WorkoutRoutineSetId)
            .IsRequired(true);
        builder.HasIndex(wr => wr.WorkoutRoutineSetId);

        
        builder.HasMany(x => x.Exercises)
            .WithOne()
            .HasForeignKey(x => x.Id);
        
        // Configurando o relacionamento com WorkoutRoutineSet
        builder.HasOne(wr => wr.WorkoutRoutineSet)
            .WithMany(wrs => wrs.WorkoutRoutines)
            .HasForeignKey(wr => wr.WorkoutRoutineSetId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}