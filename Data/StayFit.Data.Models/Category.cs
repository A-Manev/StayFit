﻿namespace StayFit.Data.Models
{
    using System.Collections.Generic;

    using StayFit.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.SubCategories = new HashSet<SubCategory>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
