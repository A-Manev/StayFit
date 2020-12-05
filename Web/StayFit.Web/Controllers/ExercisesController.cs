namespace StayFit.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using StayFit.Services.Data;
    using StayFit.Web.ViewModels.Exercises;

    public class ExercisesController : Controller
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
    }
}
