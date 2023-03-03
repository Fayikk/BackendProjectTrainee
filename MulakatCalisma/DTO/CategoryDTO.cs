using MulakatCalisma.Entity;

namespace MulakatCalisma.DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
