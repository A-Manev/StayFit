namespace StayFit.Services.HangFire.UpdateUserCalories
{
    using System.Linq;
    using System.Threading.Tasks;

    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;

    public class UpdateUserCalories : IUpdateUserCalories
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UpdateUserCalories(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task UpdateAsync()
        {
            var users = this.userRepository
                 .All()
                 .ToList();

            foreach (var user in users)
            {
                user.RemainingCalories = user.DailyCalories;
            }

            await this.userRepository.SaveChangesAsync();
        }
    }
}
