namespace StayFit.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StayFit.Data.Models;
    using StayFit.Services.Data;
    using StayFit.Web.ViewModels.Exercises;

    public class ExercisesController : BaseController
    {
        private readonly IExercisesService exercisesService;
        private readonly IWorkoutService workoutService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEquipmentService equipmentService;

        public ExercisesController(IExercisesService exercisesService, IWorkoutService workoutService, UserManager<ApplicationUser> userManager, IEquipmentService equipmentService)
        {
            this.exercisesService = exercisesService;
            this.workoutService = workoutService;
            this.userManager = userManager;
            this.equipmentService = equipmentService;
        }

        public IActionResult Index()
        {
            var viewModel = new SearchExerciseInputModel();

            viewModel.Equipments = this.equipmentService.GetAllEquipments();

            return this.View(viewModel);
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 14;

            var viewModel = new ExercisesListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                TotalCount = this.exercisesService.GetCount(),
                Exercises = this.exercisesService.GetAll<ExerciseViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var viewModel = this.exercisesService.GetExerciseDetails<ExerciseDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Workout()
        {
            var viewModel = new ExerciseInputModel
            {
                Exercises = this.exercisesService.GetAll<ExerciseDropdownViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Workout([FromBody]List<ExerciseInputModel> inputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.workoutService.CreateAsync(inputModel, user.Id);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Search(SearchExerciseInputModel inputModel, int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 15;

            var result = this.exercisesService.GetAllSearched<ExerciseViewModel>(inputModel, id);

            if (result.Count == 0)
            {
                return this.NotFound();
            }

            var viewModel = new ExercisesListViewModel
            {
                PageNumber = id,
                ItemsPerPage = ItemsPerPage,
                TotalCount = result.Count,
                Exercises = result.Exercises,
                Name = inputModel.Name,
                BodyPart = inputModel.BodyPart,
                Difficulty = inputModel.Difficulty,
                ExerciseType = inputModel.ExerciseType,
                EquipmentId = inputModel.EquipmentId,
            };

            return this.View(viewModel);
        }
    }
}
