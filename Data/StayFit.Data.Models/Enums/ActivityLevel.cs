namespace StayFit.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum ActivityLevel
    {
        [Display(Name = "Sedentary (little or no exercise)")]
        Sedentary = 1,
        [Display(Name = "Lightly active (light exercise/sports 1-3 days/week)")]
        LightlyActive = 2,
        [Display(Name = "Moderately active (moderate exercise/sports 3-5 days/week)")]
        ModeratelyActive = 3,
        [Display(Name = "Very active (hard exercise/sports 6-7 days a week)")]
        VeryActive = 4,
        [Display(Name = "Extra active (very hard exercise/sports & physical job or 2x training)")]
        ExtraActive = 5,
    }
}
