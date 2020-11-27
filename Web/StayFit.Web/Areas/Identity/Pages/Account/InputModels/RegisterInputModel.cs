namespace StayFit.Web.Areas.Identity.Pages.Account.InputModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using StayFit.Data.Models.Enums;
    using StayFit.Web.Infrastructure.CustomAttributes;

    public class RegisterInputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; }

        [Display(Name = "Current Weight")]
        public double Weight { get; set; }

        [Display(Name = "Height")]
        public int Height { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        public DateTime BirthDate => DateTime.Parse($"{this.Day} {this.Month} {this.Year}", CultureInfo.InvariantCulture);

        public int Age => DateTime.Now.Year - this.Year;

        [Required]
        [Range(1, 31)]
        public int Day { get; set; }

        [CurrentYearMaxValue(1900)]
        public int Year { get; set; }

        [Required(ErrorMessage = "Choose a valid month.")]
        [Range(1, 12)]
        public Month Month { get; set; }

        [Display(Name = "How would you describe your normal daily activities?")]
        public ActivityLevel ActivityLevel { get; set; }

        [Display(Name = "What is your goal?")]
        public WeightLoseGain WeightLoseGain { get; set; }
    }
}
