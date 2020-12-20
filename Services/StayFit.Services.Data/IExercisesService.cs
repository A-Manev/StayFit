namespace StayFit.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using StayFit.Web.ViewModels.Exercises;

    public interface IExercisesService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 14);

        T GetExerciseDetails<T>(int? id);

        int GetCount();

        IEnumerable<T> GetAll<T>();

        (IEnumerable<T> Exercises, int Count) GetAllSearched<T>(SearchExerciseInputModel input, int page, int itemsPerPage = 15);

        Task CreateAsync(CreateExerciseAdministratioInputModel inputModel);

        T GetById<T>(int? id);

        Task UpdateAsync(ExerciseAdministrationEditViewModel inputModel);

        Task DeleteAsync(int id);

        IEnumerable<T> GetAllWithDeleted<T>(int page, int itemsPerPage = 20);
    }
}
