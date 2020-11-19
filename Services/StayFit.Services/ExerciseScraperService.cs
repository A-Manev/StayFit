namespace StayFit.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AngleSharp;
    using StayFit.Data.Common.Repositories;
    using StayFit.Data.Models;
    using StayFit.Data.Models.Enums;
    using StayFit.Services.Models;

    public class ExerciseScraperService : IExerciseScraperService
    {
        private readonly IConfiguration config;
        private readonly IBrowsingContext context;

        private readonly IDeletableEntityRepository<Equipment> equipmentRepository;
        private readonly IDeletableEntityRepository<Exercise> exerciseRepository;

        public ExerciseScraperService(IDeletableEntityRepository<Equipment> equipmentRepository, IDeletableEntityRepository<Exercise> exerciseRepository)
        {
            this.config = Configuration.Default.WithDefaultLoader();
            this.context = BrowsingContext.New(this.config);

            this.equipmentRepository = equipmentRepository;
            this.exerciseRepository = exerciseRepository;
        }

        public async Task PopulateDbWithExercises(int pagesCount)
        {
            var exerciseBag = new ConcurrentBag<ExerciseDto>();

            Parallel.For(1, pagesCount, (i) =>
            {
                try
                {
                    var allExercisethis = this.GetALLExercise(i);

                    Parallel.ForEach(allExercisethis, (exercise) =>
                    {
                        exerciseBag.Add(exercise);
                    });
                }
                catch (Exception)
                {
                }
            });

            foreach (var exercise in exerciseBag)
            {
                var equipmentId = await this.GetOrCreateEquipmentAsync(exercise.EquipmentName);
                var exerciseExist = this.exerciseRepository
                    .AllAsNoTracking()
                    .Any(x => x.Name == exercise.Name);

                if (exerciseExist)
                {
                    continue;
                }

                var newExercise = new Exercise
                {
                    Name = exercise.Name,
                    ImageUrl = exercise.ImageUrl,
                    BodyPart = (BodyPart)Enum.Parse(typeof(BodyPart), exercise.BodyPart, true),
                    Difficulty = (Difficulty)Enum.Parse(typeof(Difficulty), exercise.Difficulty, true),
                    ExerciseType = (ExerciseType)Enum.Parse(typeof(ExerciseType), exercise.ExerciseType, true),
                    Description = exercise.Description,
                    Benefits = exercise.Benefits,
                    EquipmentId = equipmentId,
                };

                await this.exerciseRepository.AddAsync(newExercise);
                await this.exerciseRepository.SaveChangesAsync();
            }
        }

        private async Task<int> GetOrCreateEquipmentAsync(string equipmentName)
        {
            var equipment = this.equipmentRepository
                                .AllAsNoTracking()
                                .FirstOrDefault(x => x.Name == equipmentName);

            if (equipment == null)
            {
                equipment = new Equipment()
                {
                    Name = equipmentName,
                };

                await this.equipmentRepository.AddAsync(equipment);
                await this.equipmentRepository.SaveChangesAsync();
            }

            return equipment.Id;
        }

        private ExerciseDto GetExercise(string exerciseUrl)
        {
            var document = this.context.OpenAsync(exerciseUrl).GetAwaiter().GetResult();

            var exercise = new ExerciseDto();

            var exerciseName = document.QuerySelector("#js-ex-content > div > section.ExDetail-section.ExDetail-meta.flexo-container.flexo-start.flexo-between > div.grid-8.grid-12-s.grid-12-m > h1");

            if (exerciseName != null)
            {
                exercise.Name = exerciseName.TextContent.Trim();
            }

            var exerciseDescription = document.QuerySelector("#js-ex-content > div > section.ExDetail-section.ExDetail-meta.flexo-container.flexo-start.flexo-between > div.ExDetail-shortDescription.grid-10 > p");

            if (exerciseDescription != null)
            {
                exercise.Description = exerciseDescription.TextContent.Trim();
            }

            var benefits = document.QuerySelector("#js-ex-content > div > section.ExDetail-section.ExDetail-meta.flexo-container.flexo-start.flexo-between > div.ExDetail-benefits.grid-8 > ol");

            if (benefits != null)
            {
                var listBenefits = benefits.TextContent.Trim().Split('\n');

                var sb = new StringBuilder();

                foreach (var benefit in listBenefits)
                {
                    sb.AppendLine(benefit);
                }

                exercise.Benefits = sb.ToString().TrimEnd();
            }

            var properties = document.QuerySelectorAll(".bb-list--plain > li");

            int count = 0;

            foreach (var item in properties)
            {
                var curent = item.TextContent.Trim().Split(new string[] { "\n", ":\n", " " }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                if (count == 0 && curent != null)
                {
                    if (curent.Length == 2)
                    {
                        exercise.ExerciseType = curent[1];
                    }
                    else
                    {
                        exercise.ExerciseType = curent[1] + curent[2];
                    }
                }
                else if (count == 1 && curent != null)
                {
                    if (curent.Length == 4)
                    {
                        exercise.BodyPart = curent[3];
                    }
                    else
                    {
                        exercise.BodyPart = curent[3] + curent[4];
                    }
                }
                else if (count == 2 && curent != null)
                {
                    exercise.EquipmentName = curent[1];
                }
                else if (count == 3 && curent != null)
                {
                    exercise.Difficulty = curent[1];
                }

                count++;
            }

            var imageUrl = document.QuerySelector("#js-ex-content > div > section.ExDetail-section.ExDetail-photos.paywall__xdb-details > div.flexo-container.flexo-around.flexo-no-wrap > div:nth-child(1) > img").GetAttribute("src");

            if (imageUrl != null)
            {
                exercise.ImageUrl = imageUrl;
            }

            return exercise;
        }

        private ConcurrentBag<ExerciseDto> GetALLExercise(int id)
        {
            var bag = new ConcurrentBag<ExerciseDto>();

            var page = this.context
                .OpenAsync($"https://www.bodybuilding.com/exercises/finder/{id}")
                .GetAwaiter()
                .GetResult();

            var exeUrlQuery = page.QuerySelectorAll("div.ExResult-cell.ExResult-cell--nameEtc > h3 > a");

            foreach (var item in exeUrlQuery)
            {
                var link = item.QuerySelector("#js-ex-category-body > div.ExCategory-results > div:nth-child(2) > div.ExResult-cell.ExResult-cell--nameEtc > h3 > a");

                var url = item.OuterHtml
                    .ToString()
                    .Split(new string[] { "\"" }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var exerciseUrl = "https://www.bodybuilding.com" + url[1];

                bag.Add(this.GetExercise(exerciseUrl));
            }

            return bag;
        }
    }
}
