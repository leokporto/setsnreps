using System.ComponentModel.DataAnnotations.Schema;
using SetsnReps.Core.Models.Exercise;

namespace MegaGymImporter.Models;

public class MegaGymRow
{
    public string Title { get; set; }
    public string BodyPart { get; set; }
    public string Equipment { get; set; }
}

public class ExerciseRow : Exercise
{
}