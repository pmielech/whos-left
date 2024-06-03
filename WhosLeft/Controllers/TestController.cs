using Microsoft.AspNetCore.Mvc;
using WhosLeft.Models;

namespace WhosLeft.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            var data = new TestViewModel { Name = "John", BirthDate = new DateTime(1954, 3, 10) };
            return View(data);
        }
    }
}