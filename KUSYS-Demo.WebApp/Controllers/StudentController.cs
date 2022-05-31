using KUSYS_Demo.Services.Abstract;
using KUSYS_Demo.WebApp.Models;
using KUSYS_Demo.WebApp.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KUSYS_Demo.WebApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentController(IStudentService studentService, IHttpContextAccessor httpContextAccessor)
        {
            _studentService = studentService;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Transaction
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index()
        {
            //
            var studentId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (studentId == null) // Admin
            {
                var res = await _studentService.ToListAsync();
                var viewModel = res.Select(c => new StudentViewModel()
                {
                    StudentId = c.StudentId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    BirthDate = c.BirthDate,
                }).ToList();
                return View(viewModel);
            }
            else // user
            {
                var res = await _studentService.Find(Convert.ToInt32(studentId));
                var viewModel = new List<StudentViewModel>() {
                    new StudentViewModel()
                    {
                        StudentId = res.StudentId,
                        FirstName = res.FirstName,
                        LastName = res.LastName,
                        BirthDate = res.BirthDate
                    }
                };

                return View(viewModel);
            }

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateOrUpdate(int id = 0)
        {
            if (id == 0)
                return View(new StudentViewModel());
            else
            {
                var getRes = await _studentService.Find(id);
                if (getRes == null)
                    return View(new StudentViewModel());
                else
                    return View(new StudentViewModel()
                    {
                        StudentId = getRes.StudentId,
                        FirstName = getRes.FirstName,
                        LastName = getRes.LastName,
                        BirthDate = getRes.BirthDate,
                    });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrUpdate(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.StudentId == 0)
                {
                    await _studentService.Create(new Data.Entities.Student()
                    {
                        StudentId = model.StudentId,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        BirthDate = (DateTime)model.BirthDate,
                    });
                }
                else
                    await _studentService.Update(new Data.Entities.Student()
                    {
                        StudentId = model.StudentId,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        BirthDate = (DateTime)model.BirthDate,
                    });
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentService.Delete(id);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Detail(int id = 0)
        {
            var getRes = await _studentService.Find(id);

            var model = new StudentViewModel()
            {
                StudentId = getRes.StudentId,
                FirstName = getRes.FirstName,
                LastName = getRes.LastName,
                BirthDate = getRes.BirthDate,
            };

            return Json(new
            {
                title = "Başarılı",
                message = "Başarılı",
                status = "Successful",
                Page = this.RenderViewToString("Modal/_DetailModal", model)
            });
        }




    }
}
