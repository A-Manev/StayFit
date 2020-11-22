namespace StayFit.Data.Models
{
    using System.Collections.Generic;

    using StayFit.Data.Common.Models;

    public class SubCategory : BaseDeletableModel<int>
    {
        public SubCategory()
        {
            this.Meals = new HashSet<Meal>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Meal> Meals { get; set; }
    }
}
