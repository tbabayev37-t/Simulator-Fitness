using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Simulator16TB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
