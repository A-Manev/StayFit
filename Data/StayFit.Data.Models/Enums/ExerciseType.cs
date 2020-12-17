namespace StayFit.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum ExerciseType
    {
        // Undefined = 0,
        Cardio = 1,
        Hypertrophy = 2,
        [Display(Name = "Olympic Weightliftin")]
        OlympicWeightlifting = 3,
        Plyometrics = 4,
        Powerlifting = 5,
        Strength = 6,
        Stretching = 7,
        Strongman = 8,
    }
}
