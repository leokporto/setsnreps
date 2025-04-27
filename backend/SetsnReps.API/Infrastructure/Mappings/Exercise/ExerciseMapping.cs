using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetsnReps.Core.Models.Exercise;

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
        
        builder.HasOne(x => x.PrimaryMuscleGroup)
            .WithMany()
            .HasForeignKey(x => x.PrimaryMuscleGroupId);

        builder.HasOne(x => x.EquipmentType)
            .WithMany()
            .HasForeignKey(x => x.EquipmentTypeId);
    }
}