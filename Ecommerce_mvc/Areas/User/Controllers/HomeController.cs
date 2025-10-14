using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_mvc.Models;
using Ecommerce_mvc.Data;

namespace Ecommerce_mvc.Areas.User.Controllers;
[Area("User")]
public class HomeController : Controller
{


    private readonly ILogger<HomeController> _logger;
    ApplicationDbContext _context = new ApplicationDbContext();



    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // var cats = _context.Categories.ToList();
        ViewBag.Categories = _context.Categories.ToList();
        return View("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
