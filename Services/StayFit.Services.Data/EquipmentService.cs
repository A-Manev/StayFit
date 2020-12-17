namespace StayFit.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;
    using StayFit.Web.ViewModels.Exercises;

    public class EquipmentService : IEquipmentService
    {
        private readonly IDeletableEntityRepository<Equipment> equipmentRepository;

        public EquipmentService(IDeletableEntityRepository<Equipment> equipmentRepository)
        {
            this.equipmentRepository = equipmentRepository;
        }

        public IEnumerable<EquipmentViewModel> GetAllEquipments()
        {
            return this.equipmentRepository
                .AllAsNoTracking()
                .To<EquipmentViewModel>()
                .OrderBy(x => x.Name)
                .ToList();
        }
    }
}
