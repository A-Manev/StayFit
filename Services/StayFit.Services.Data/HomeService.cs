namespace StayFit.Services.Data
{
    using System.Linq;

    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Services.Mapping;

    public class HomeService : IHomeService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public HomeService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public T GetUserInfo<T>(string id)
        {
            return this.userRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }
    }
}
