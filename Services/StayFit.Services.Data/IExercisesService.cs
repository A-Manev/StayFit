namespace StayFit.Services.Data
{
    using System.Collections.Generic;

    public interface IExercisesService
    {
        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 14);

        T GetExerciseDetails<T>(int id);

        int GetCount();

        IEnumerable<T> GetAll<T>();
    }
}
