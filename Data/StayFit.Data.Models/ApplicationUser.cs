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

        public decimal DailyCalories { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; }

        public virtual ICollection<UserMeal> UserMeal { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
