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


        public IActionResult Delete(int id)
        {
            var cat = context.Categories.Find(id);
            context.Categories.Remove(cat);
            context.SaveChanges();
            return RedirectToAction("Index");
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

        public IActionResult Edit(int id)
        {

            var cat = context.Categories.Find(id);
            return View(cat);
        }
        public IActionResult Update(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", category);
            }
            var cat = context.Categories.Find(category.Id);
            cat.Id = category.Id;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
   


    }
}
