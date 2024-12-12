using Microsoft.AspNetCore.Mvc;

namespace W11_CrazyMusicians.Controllers;

public class MusiciansController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}