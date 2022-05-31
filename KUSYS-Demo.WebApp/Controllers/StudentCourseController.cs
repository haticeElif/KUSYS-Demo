using KUSYS_Demo.Services.Abstract;
using KUSYS_Demo.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace KUSYS_Demo.WebApp.Controllers
{
    public class StudentCourseController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService; private readonly IHttpContextAccessor _httpContextAccessor;

        public StudentCourseController(IStudentService studentService, ICourseService courseService, IHttpContextAccessor httpContextAccessor)
        {
            _studentService = studentService;
            _courseService = courseService;
            _httpContextAccessor = httpContextAccessor;

        }

        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Index()
        {
            var viewModel = StudentListAsync();
            return View(viewModel.Result);
        }

        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> CreateOrUpdate(int id = 0)
        {
            var listRes = await _courseService.ToListAsync();

            if (id == 0)
            {
                var listStudentRes = await _studentService.ToListNoCourseAsync();
                return View(new StudentViewModel()
                {
                    StudentList = listStudentRes.Select(x => new Itemlist { Value = x.StudentId.ToString(), Text = x.FirstName + " " + x.LastName }).ToList(),
                    CourseList = listRes.Select(x => new Itemlist { Value = x.CourseId.ToString(), Text = x.CourseName }).ToList()
                });
            }
            else
            {
                var listStudentRes = await _studentService.ToListAsync();
                var getRes = await _studentService.Find(id);
                if (getRes == null)
                    return View(new StudentViewModel()
                    {
                        StudentList = listStudentRes.Select(x => new Itemlist { Value = x.StudentId.ToString(), Text = x.FirstName + " " + x.LastName }).ToList(),
                        CourseList = listRes.Select(x => new Itemlist { Value = x.CourseId.ToString(), Text = x.CourseName }).ToList()
                    });
                else
                    return View(new StudentViewModel()
                    {
                        StudentId = getRes.StudentId,
                        FirstName = getRes.FirstName,
                        LastName = getRes.LastName,
                        BirthDate = getRes.BirthDate,
                        CourseId = getRes.CourseId,
                        StudentList = listStudentRes.Select(x => new Itemlist { Value = x.StudentId.ToString(), Text = x.FirstName + " " + x.LastName }).ToList(),
                        CourseList = listRes.Select(x => new Itemlist { Value = x.CourseId.ToString(), Text = x.CourseName }).ToList()
                    });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrUpdate(StudentViewModel model)
        {
            var student = _studentService.Find(model.StudentId);
            await _studentService.Update(new Data.Entities.Student()
            {
                StudentId = model.StudentId,
                CourseId = model.CourseId,
                FirstName = student.Result.FirstName,
                LastName = student.Result.LastName,
                BirthDate = (DateTime)student.Result.BirthDate,
            });

            var listStudentRes = await _studentService.ToListNoCourseAsync();
            var listRes = await _courseService.ToListAsync();

            model.StudentList = listStudentRes.Select(x => new Itemlist { Value = x.StudentId.ToString(), Text = x.FirstName + " " + x.LastName }).ToList();
            model.CourseList = listRes.Select(x => new Itemlist { Value = x.CourseId.ToString(), Text = x.CourseName }).ToList();

            return RedirectToAction(nameof(Index));
        }


        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _courseService.Delete(id);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> StudentFullList()
        {
            var viewModel = StudentListAsync();
            return View(viewModel.Result);
        }

        public async Task<List<StudentViewModel>> StudentListAsync()
        {
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
                    CourseName = c.Course == null ? "" : c.Course.CourseName,
                    Role = "Admin"
                }).ToList();
                return viewModel;
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
                        BirthDate = res.BirthDate,
                        CourseName = res.Course == null ? "" : res.Course.CourseName,
                        Role = "User"
                    }
                };
                return viewModel;
            }
        }

    }
}
