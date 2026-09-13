using ISS_TEST.Models;
using ISS_TEST.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISS_TEST.Controller
{
    public class MasterController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMasterService _service;

        public MasterController(IMasterService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Create() => View(new MasterModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MasterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!await _service.InsertAsync(model))
            {
                return View(model);
            }

            TempData["Success"] = "Record created successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            if (id <= 0) return BadRequest();

            var model = await _service.GetByIdAsync(id);
            return model is null ? NotFound() : View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MasterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (await _service.GetByIdAsync(model.Id) is null)
                return NotFound();

            await _service.UpdateAsync(model);

            TempData["Success"] = "Record updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            if (id <= 0) return BadRequest();

            if (!await _service.DeleteAsync(id))
                return NotFound();
            await _service.DeleteAsync(id);
            TempData["Success"] = "Record deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
