namespace StayFit.Services.Data
{
    using System.Collections.Generic;

    using StayFit.Web.ViewModels.Exercises;

    public interface IEquipmentService
    {
        IEnumerable<EquipmentViewModel> GetAllEquipments();
    }
}
