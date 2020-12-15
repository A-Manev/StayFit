namespace StayFit.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

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
    }
}
