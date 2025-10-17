using Ecommerce_mvc.Data;
using Ecommerce_mvc.Models;
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
        public IActionResult Creat()
        {

            return View(new Category());
        }
        public IActionResult Store(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View("Creat",category);
            }

            context.Categories.Add(category);
            
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Remove(int id)
        {
            var cats = context.Categories.Find(id);
            context.Categories.Remove(cats);
            context.SaveChanges();
            return RedirectToAction("Index");


        }


        public IActionResult Update(int id)
        {
            var cat = context.Categories.Find(id);
            return View("Update",cat);
        }

        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", new Category());
            }
            var cat = context.Categories.Find(category.Id);
            cat.Name = category.Name;
            context.SaveChanges();
            return RedirectToAction("Index");
          
        }
    }
}
