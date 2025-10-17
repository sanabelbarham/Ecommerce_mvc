using System.ComponentModel.DataAnnotations;

namespace Ecommerce_mvc.Models
{
    public class Category
    {

        public int Id { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(20)]
        public string Name { get; set; }
        public List<Product> Products = new List<Product>();
    }
}
