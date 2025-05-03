using SetsnReps.Core.DTOs.Exercise;
using DomainExercise = SetsnReps.Domain.Entities.Exercise;

namespace SetsnReps.API.Mappings.Exercise;

public static class ExerciseExtensions
{
    public static ExerciseResponse ToExerciseResponse(this DomainExercise.Exercise exercise)
    {
        return new ExerciseResponse()
        {
            Id = exercise.Id,
            Name = exercise.Name,
            MuscleGroupId = exercise.MuscleGroupId,
            MuscleGroup = exercise.MuscleGroup.Name,
            EquipmentTypeId = exercise.EquipmentTypeId,
            EquipmentType = exercise.EquipmentType.Name,
            ThumbnailUri = exercise.ThumbnailUri
        };
    }
}