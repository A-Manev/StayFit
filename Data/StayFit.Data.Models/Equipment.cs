namespace StayFit.Data.Models
{
    using StayFit.Data.Common.Models;

    public class Equipment : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}
