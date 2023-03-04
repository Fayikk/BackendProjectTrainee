using MulakatCalisma.Entity;
using System.ComponentModel.DataAnnotations;

namespace MulakatCalisma.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Guid Image { get; set; } = Guid.NewGuid();

    }
}
