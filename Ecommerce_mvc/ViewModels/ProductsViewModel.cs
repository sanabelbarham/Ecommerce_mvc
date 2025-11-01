using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_mvc.ViewModels
{
    public class ProductsViewModel
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
        public string CategoryName { get; set; }
       
        public string Description { get; set; }
        [ValidateNever]

        public string ImageUrl { get; set; }
        public decimal Price { get; set; }



    }
}
