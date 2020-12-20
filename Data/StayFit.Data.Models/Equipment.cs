namespace StayFit.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using StayFit.Data.Common.Models;

    public class Equipment : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }
    }
}
