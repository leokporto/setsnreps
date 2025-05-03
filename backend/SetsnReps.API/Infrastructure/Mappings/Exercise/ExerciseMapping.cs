using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetsnReps.Domain.Entities.Exercise;

namespace SetsnReps.API.Infrastructure.Mappings;

public class ExerciseMapping : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable("Exercises");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.Name)
            .IsRequired(true);
        
        builder.Property(x => x.ThumbnailUri)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.Property(e => e.EquipmentTypeId)
            .IsRequired();
        builder.HasIndex(e => e.EquipmentTypeId);

        builder.Property(m => m.PrimaryMuscleGroupId)
            .IsRequired();
        builder.HasIndex(m => m.PrimaryMuscleGroupId);
        
        builder.HasOne(x => x.EquipmentType)
            .WithMany()
            .HasForeignKey(x => x.EquipmentTypeId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.PrimaryMuscleGroup)
            .WithMany()
            .HasForeignKey(x => x.PrimaryMuscleGroupId)
            .OnDelete(DeleteBehavior.NoAction);

        
    }
}