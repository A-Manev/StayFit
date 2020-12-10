// ReSharper disable VirtualMemberCallInConstructor
namespace StayFit.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using StayFit.Data.Common.Models;
    using StayFit.Data.Models.Enums;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.Workouts = new HashSet<Workout>();
            this.UserMeal = new HashSet<UserMeal>();
            this.Votes = new HashSet<Vote>();
            this.Comments = new HashSet<Comment>();
            this.AddedByUserMeals = new HashSet<Meal>();
            this.MealsDiary = new HashSet<MealDiary>();
            this.LikedComments = new HashSet<Like>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        // User extend
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public double CurrentWeight { get; set; }

        public int Height { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public ActivityLevel ActivityLevel { get; set; }

        public WeightLoseGain WeightLoseGain { get; set; }

        public double DailyCalories { get; set; }

        public double RemainingCalories { get; set; }

        public double Protein { get; set; }

        public double RemainingProtein { get; set; }

        public double Carbs { get; set; }

        public double RemainingCarbs { get; set; }

        public double Fat { get; set; }

        public double RemainingFat { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; }

        public virtual ICollection<UserMeal> UserMeal { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Meal> AddedByUserMeals { get; set; }

        public virtual ICollection<MealDiary> MealsDiary { get; set; }

        public virtual ICollection<Like> LikedComments { get; set; }
    }
}
