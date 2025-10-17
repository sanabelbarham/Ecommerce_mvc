using Ecommerce_mvc.Data;
using Ecommerce_mvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            var products = context.Products.ToList();
            return View(products);
        }
        public IActionResult Creat()
        {
            ViewBag.Categories = context.Categories.ToList();
            return View(new Product());
        }
        public IActionResult Store(Product products,IFormFile file)
        {
            if(file !=null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                fileName += Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images",fileName);


                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                products.Image = fileName;


                context.Products.Add(products);
                context.SaveChanges();
                return RedirectToAction("Index");



            }
            return View("Creat");


        }



    }
}
