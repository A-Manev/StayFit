namespace StayFit.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;
    using StayFit.Web.ViewModels.Exercises;

    public class ExercisesService : IExercisesService
    {
        private readonly IDeletableEntityRepository<Exercise> exerciseRepository;

        public ExercisesService(IDeletableEntityRepository<Exercise> exerciseRepository)
        {
            this.exerciseRepository = exerciseRepository;
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 14)
        {
            return this.exerciseRepository
                .AllAsNoTracking()
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public T GetExerciseDetails<T>(int id)
        {
            return this.exerciseRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public int GetCount()
        {
            return this.exerciseRepository.All().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.exerciseRepository
               .AllAsNoTracking()
               .OrderBy(x => x.Name)
               .To<T>()
               .ToList();
        }

        public (IEnumerable<T> Exercises, int Count) GetAllSearched<T>(SearchExerciseInputModel input, int page, int itemsPerPage = 15)
        {
            var query = this.exerciseRepository.All().AsQueryable();

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                query = query.Where(x => x.Name.Contains(input.Name));
            }

            if (input.BodyPart != 0)
            {
                query = query.Where(x => x.BodyPart == input.BodyPart);
            }

            if (input.EquipmentId != 0)
            {
                query = query.Where(x => x.Equipment.Id == input.EquipmentId);
            }

            if (input.Difficulty != 0)
            {
                query = query.Where(x => x.Difficulty == input.Difficulty);
            }

            if (input.ExerciseType != 0)
            {
                query = query.Where(x => x.ExerciseType == input.ExerciseType);
            }

            return (query.OrderByDescending(x => x.Id)
               .Skip((page - 1) * itemsPerPage).Take(itemsPerPage).To<T>().ToList(), query.ToList().Count);
        }
    }
}
