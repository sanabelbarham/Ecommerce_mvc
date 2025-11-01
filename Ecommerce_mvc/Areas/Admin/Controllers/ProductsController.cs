using Ecommerce_mvc.Data;
using Ecommerce_mvc.Models;
using Ecommerce_mvc.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            //var products = context.Products.Join(context.Categories, p => p.CategoryId, c => c.Id,
            //               (p, c) => new
            //               {

            //                   p.Name,
            //                   p.Image,
            //                   p.Price,
            //                   p.Id,
            //                   CategpryName = c.Name,
            //                   p.Description
            //               }

            //               );

            //another way to write the join 
            //now the navigation properity Category hav data becouse of the include

            var products = context.Products.Include(p=>p.Category).ToList();

            var viewModelProducts = new List<ProductsViewModel>();
            foreach (var item in products)
            {
                var item1 = new ProductsViewModel
                {
                    Name = item.Name,
                    CategoryName = item.Category.Name,
             
                    Id = item.Id,
                    Description = item.Description,
                    Price = item.Price,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/Images/{item.Image}"
                };
                viewModelProducts.Add(item1);
                
            }
            return View(viewModelProducts);
        }
        public IActionResult Creat()
        {
           
            ViewBag.Categories = context.Categories.ToList();
            return View(new Product());
        }
        //[ValidateAntiForgeryToken]
        public IActionResult Store(Product products,IFormFile file)
        {
            ViewBag.Categories = context.Categories.ToList();
            ModelState.Remove("File");

            if (!ModelState.IsValid)
            {

                return View("Creat",products);
            }
            if(file==null || file.Length == 0)
            {
                ModelState.AddModelError("Image", "please uplode an Image");
                return View("Creat", products);

            }
            var allowedExtention = new[] { ".jpg", ".webp" };
            var extention = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtention.Contains(extention))
            {
                ModelState.AddModelError("Image", "use only .jpg, .webp extentions");
                return View("Creat", products);

            }
            if (file.Length >10 * 1024 *   1024)
            {
                ModelState.AddModelError("Image", "Image size must be less than 10MB");
                return View("Creat", products);

            }


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
            ViewBag.Categories = context.Categories.ToList();

            return RedirectToAction("Index");


        }

        public IActionResult Edit(int id)
        {

            var product = context.Products.Find(id);
            ViewBag.Categories = context.Categories.ToList();

            return View(product);
        } 
        
        
        public IActionResult Update(Product product,IFormFile?file)
        {
            ViewBag.Categories = context.Categories.ToList();


            var pro = context.Products.Find(product.Id);
            pro.Quantity = product.Quantity;
            pro.Name = product.Name;
            pro.Description = product.Description;
            pro.Price = product.Price;
            pro.Rate = product.Rate;
            pro.Name = product.Name;
            pro.CategoryId = product.CategoryId;
          
            if(file!=null && file.Length > 0)
            {
                var oldfilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", pro.Image);
                System.IO.File.Delete(oldfilePath);

                var fileName = Guid.NewGuid().ToString();
                fileName += Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                pro.Image = fileName;

            }
            context.SaveChanges();

            ViewBag.Categories = context.Categories.ToList();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {

            var pro = context.Products.Find(id);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", pro.Image);
            System.IO.File.Delete(filePath);
            context.Products.Remove(pro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }




    }
}
