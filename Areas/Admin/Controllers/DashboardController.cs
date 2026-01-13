using Microsoft.AspNetCore.Mvc;

namespace Simulator16TB.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
