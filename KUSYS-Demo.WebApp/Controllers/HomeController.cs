using KUSYS_Demo.Services.Abstract;
using KUSYS_Demo.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KUSYS_Demo.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentService _studentService;


        public HomeController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Authorize(Roles = "Admin,User")]
        public virtual async Task<IActionResult> Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Authorize(Roles = "Admin,User")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}