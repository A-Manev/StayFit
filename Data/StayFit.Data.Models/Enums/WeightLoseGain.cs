namespace StayFit.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum WeightLoseGain
    {
        [Display(Name = "Lose 1 kilogram per week")]
        Lose1kg = 1,
        [Display(Name = "Lose 0.75 kilograms per week")]
        Lose075kgs = 2,
        [Display(Name = "Lose 0.5 kilograms per week")]
        Lose05kgs = 3,
        [Display(Name = "Lose 0.25 kilograms per week")]
        Lose025kgs = 4,
        [Display(Name = "Maintain my current weight")]
        Maintain = 5,
        [Display(Name = "Gain 0.25 kilograms per week")]
        Gain025kgs = 6,
        [Display(Name = "Gain 0.5 kilograms per week")]
        Gain05kgs = 7,
    }
}
