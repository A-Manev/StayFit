namespace StayFit.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Data.Models.Enums;
    using StayFit.Services.Mapping;
    using StayFit.Web.ViewModels.Users;

    public class UsersService : IUsersService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png" };
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IRepository<Image> imageRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> userRepository, IRepository<Image> imageRepository)
        {
            this.userRepository = userRepository;
            this.imageRepository = imageRepository;
        }

        public double CalculateUserCalories(ApplicationUser user)
        {
            double totalCalories = 0.0;

            if (user.Gender == Gender.Male)
            {
                if (user.ActivityLevel == ActivityLevel.Sedentary)
                {
                    totalCalories = this.CalculateForMale(user, 1.2);
                }
                else if (user.ActivityLevel == ActivityLevel.LightlyActive)
                {
                    totalCalories = this.CalculateForMale(user, 1.375);
                }
                else if (user.ActivityLevel == ActivityLevel.ModeratelyActive)
                {
                    totalCalories = this.CalculateForMale(user, 1.55);
                }
                else if (user.ActivityLevel == ActivityLevel.VeryActive)
                {
                    totalCalories = this.CalculateForMale(user, 1.725);
                }
                else if (user.ActivityLevel == ActivityLevel.ExtraActive)
                {
                    totalCalories = this.CalculateForMale(user, 1.9);
                }
            }
            else if (user.Gender == Gender.Female)
            {
                if (user.ActivityLevel == ActivityLevel.Sedentary)
                {
                    totalCalories = this.CalculateForFemale(user, 1.2);
                }
                else if (user.ActivityLevel == ActivityLevel.LightlyActive)
                {
                    totalCalories = this.CalculateForFemale(user, 1.375);
                }
                else if (user.ActivityLevel == ActivityLevel.ModeratelyActive)
                {
                    totalCalories = this.CalculateForFemale(user, 1.55);
                }
                else if (user.ActivityLevel == ActivityLevel.VeryActive)
                {
                    totalCalories = this.CalculateForFemale(user, 1.725);
                }
                else if (user.ActivityLevel == ActivityLevel.ExtraActive)
                {
                    totalCalories = this.CalculateForFemale(user, 1.9);
                }
            }

            if (user.WeightLoseGain == WeightLoseGain.Lose1kg)
            {
                totalCalories /= 2.2;
            }
            else if (user.WeightLoseGain == WeightLoseGain.Lose075kgs)
            {
                totalCalories /= 1.7;
            }
            else if (user.WeightLoseGain == WeightLoseGain.Lose05kgs)
            {
                totalCalories /= 1.1;
            }
            else if (user.WeightLoseGain == WeightLoseGain.Lose025kgs)
            {
                totalCalories -= totalCalories / 6; // 0.6
            }
            else if (user.WeightLoseGain == WeightLoseGain.Gain025kgs)
            {
                totalCalories += totalCalories / 6; // 0.6
            }
            else if (user.WeightLoseGain == WeightLoseGain.Gain05kgs)
            {
                totalCalories *= 1.1;
            }

            return totalCalories;
        }

        public double CalculateForMale(ApplicationUser user, double activityLevelValue)
        {
            return (activityLevelValue * (66.5 + (13.75 * user.CurrentWeight))) + (5.003 * user.Height) - (6.755 * user.Age);
        }

        public double CalculateForFemale(ApplicationUser user, double activityLevelValue)
        {
            return (activityLevelValue * (665 + (9.563 * user.CurrentWeight))) + (1.850 * user.Height) - (4.676 * user.Age);
        }

        public T GetById<T>(string userId)
        {
            return this.userRepository
                .All()
                .Where(x => x.Id == userId)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task UploadImageAsync(HomePageUserViewModel inputModel, string userId, string imagePath)
        {
            var user = this.userRepository.AllAsNoTracking().Where(x => x.Id == userId).FirstOrDefault();

            Directory.CreateDirectory($"{imagePath}/users/");

            var extension = Path.GetExtension(inputModel.Image.FileName).TrimStart('.');

            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var newImage = new Image
            {
                AddedByUserId = userId,
                Extension = extension,
                MealId = 400,
            };

            await this.imageRepository.AddAsync(newImage);
            await this.imageRepository.SaveChangesAsync();

            user.Images.Add(newImage);

            var physicalPath = $"{imagePath}/users/{newImage.Id}.{extension}";
            using var fileStream = new FileStream(physicalPath, FileMode.Create);
            await inputModel.Image.CopyToAsync(fileStream);

            await this.userRepository.SaveChangesAsync();
        }
    }
}
