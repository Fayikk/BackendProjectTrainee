using System.ComponentModel.DataAnnotations;

namespace MulakatCalisma.Entity
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
