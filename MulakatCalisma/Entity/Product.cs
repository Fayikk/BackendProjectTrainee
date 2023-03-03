using System.ComponentModel.DataAnnotations;

namespace MulakatCalisma.Entity
{
    public class Product
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
