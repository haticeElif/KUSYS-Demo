using KUSYS_Demo.Services.Abstract;
using KUSYS_Demo.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.WebApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _CourseService;

        public CourseController(ICourseService CourseService)
        {
            _CourseService = CourseService;
        }

        // GET: Transaction
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var res = await _CourseService.ToListAsync();
            var viewModel = res.Select(c => new CourseViewModel()
            {
                CourseId = c.CourseId,
                CourseName = c.CourseName,
            }).ToList();
            return View(viewModel);
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateOrUpdate(string id)
        {
            if (id == "0")
                return View(new CourseViewModel() );
            else
            {
                var getRes = await _CourseService.Find(id);
                if (getRes == null)
                    return View(new CourseViewModel());
                else
                    return View(new CourseViewModel()
                    {
                        CourseId = getRes.CourseId,
                        CourseName = getRes.CourseName,
                    });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrUpdate(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var getRes = await _CourseService.Find(model.CourseId);
                if (getRes == null) 
                {
                    await _CourseService.Create(new Data.Entities.Course()
                    {
                        CourseId = model.CourseId,
                        CourseName = model.CourseName,
                    });
                }
                else
                    await _CourseService.Update(new Data.Entities.Course()
                    {
                        CourseId = model.CourseId,
                        CourseName = model.CourseName,
                    });
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _CourseService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
