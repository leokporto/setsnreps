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

        builder.Property(m => m.MuscleGroupId)
            .IsRequired();
        builder.HasIndex(m => m.MuscleGroupId);
        
        builder.HasOne(x => x.EquipmentType)
            .WithMany()
            .HasForeignKey(x => x.EquipmentTypeId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.MuscleGroup)
            .WithMany()
            .HasForeignKey(x => x.MuscleGroupId)
            .OnDelete(DeleteBehavior.NoAction);

        
    }
}