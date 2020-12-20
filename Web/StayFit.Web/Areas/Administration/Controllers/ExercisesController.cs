namespace StayFit.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using StayFit.Services.Data;
    using StayFit.Web.ViewModels.Exercises;

    [Area("Administration")]
    public class ExercisesController : AdministrationController
    {
        private readonly IExercisesService exercisesService;
        private readonly IEquipmentService equipmentService;

        public ExercisesController(
            IExercisesService exercisesService,
            IEquipmentService equipmentService)
        {
            this.exercisesService = exercisesService;
            this.equipmentService = equipmentService;
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 20;

            var viewModel = new ExercisesAdministrationListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                TotalCount = this.exercisesService.GetCount(),
                Exercises = this.exercisesService.GetAllWithDeleted<ExercisesAdministrationInListViewModel>(id),
            };

            return this.View(viewModel);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var viewModel = this.exercisesService.GetExerciseDetails<ExerciseAdministrationDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new CreateExerciseAdministratioInputModel();

            viewModel.Equipments = this.equipmentService.GetAllEquipments();

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateExerciseAdministratioInputModel inputModel)
        {
            var viewModel = new CreateExerciseAdministratioInputModel();

            if (!this.ModelState.IsValid)
            {
                viewModel.Equipments = this.equipmentService.GetAllEquipments();

                return this.View(inputModel);
            }

            await this.exercisesService.CreateAsync(inputModel);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var viewModel = this.exercisesService.GetById<ExerciseAdministrationEditViewModel>(id);

            viewModel.Equipments = this.equipmentService.GetAllEquipments();

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ExerciseAdministrationEditViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.Equipments = this.equipmentService.GetAllEquipments();

                return this.View(inputModel);
            }

            await this.exercisesService.UpdateAsync(inputModel);

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var viewModel = this.exercisesService.GetById<ExerciseAdministrationDetailsViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.exercisesService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
