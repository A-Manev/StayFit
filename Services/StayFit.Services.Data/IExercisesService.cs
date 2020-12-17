namespace StayFit.Services.Data
{
    using System.Collections.Generic;

    using StayFit.Web.ViewModels.Exercises;

    public interface IExercisesService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 14);

        T GetExerciseDetails<T>(int id);

        int GetCount();

        IEnumerable<T> GetAll<T>();

        (IEnumerable<T> Exercises, int Count) GetAllSearched<T>(SearchExerciseInputModel input, int page, int itemsPerPage = 15);
    }
}
