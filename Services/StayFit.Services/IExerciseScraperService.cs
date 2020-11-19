namespace StayFit.Services
{
    using System.Threading.Tasks;

    public interface IExerciseScraperService
    {
        Task PopulateDbWithExercises(int pagesCount);
    }
}
