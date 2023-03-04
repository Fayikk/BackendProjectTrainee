using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MulakatCalisma.Entity
{
    public class Product
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName ="Decimal(18,2)")]
        public decimal Price { get; set; }  
        public Guid Image { get; set; }=Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
