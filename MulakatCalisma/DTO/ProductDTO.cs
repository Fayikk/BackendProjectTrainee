using MulakatCalisma.Entity;
using System.ComponentModel.DataAnnotations;

namespace MulakatCalisma.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
