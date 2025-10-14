using Ecommerce_mvc.Data;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_mvc.Areas.Admin.Controllers
{
    

    [Area("Admin")]
    public class CategoriesController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
     
        public IActionResult Index()
        {
            var cats = context.Categories.ToList();
            return View(cats);
        }
    }
}
