namespace StayFit.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using StayFit.Services.Data;
    using StayFit.Web.ViewModels.Exercises;
    using System.Collections.Generic;

    public class ExercisesController : BaseController
    {
        private readonly IExercisesService exercisesService;

        public ExercisesController(IExercisesService exercisesService)
        {
            this.exercisesService = exercisesService;
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
                MealsCount = this.exercisesService.GetCount(),
                Exercises = this.exercisesService.GetAll<ExerciseViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var viewModel = this.exercisesService.GetExerciseDetails<ExerciseDetailsViewModel>(id);

            return this.View(viewModel);
        }

        public IActionResult Workout()
        {
            var viewModel = new ExerciseInputModel
            {
                Exercises = this.exercisesService.GetAll<ExerciseDropdownViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Workouts(List<ExerciseInputModel> inputModel)
        {
            return this.Ok();
        }
    }
}
