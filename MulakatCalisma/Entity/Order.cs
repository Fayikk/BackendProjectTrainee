using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MulakatCalisma.Entity
{

    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; } 
        public int ProductId { get; set; }  
        public string ProductName { get; set; }
        [Column(TypeName = "Decimal(18,2)")]
        public decimal ProductPrice { get; set; }
        public bool Status { get; set; }
        public decimal TotalPrice { get; set; } 
    }
}
