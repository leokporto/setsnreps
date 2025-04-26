using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetsnReps.Core.Models.Exercise;

namespace SetsnReps.API.Infrastructure.Mappings;

public class EquipmentTypeMapping : IEntityTypeConfiguration<EquipmentType>
{
    public void Configure(EntityTypeBuilder<EquipmentType> builder)
    {
        builder.ToTable("EquipmentTypes");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired(true);
        
        builder.HasOne(x => x.MainMuscleAffected)
            .WithMany()
            .HasForeignKey(x => x.MainMuscleAffectedId);
    }
}