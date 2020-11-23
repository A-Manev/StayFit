namespace StayFit.Services
{
    using System.Threading.Tasks;

    public interface IMealScraperService
    {
        Task PopulateDbWithMeal(int pagesCount);
    }
}
