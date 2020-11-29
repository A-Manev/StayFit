namespace StayFit.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum SkillLevel
    {
        Easy = 1,
        [Display(Name = "More Effort")]
        MoreEffort = 2,
        [Display(Name = "A Challenge")]
        AChallenge = 3,
    }
}
