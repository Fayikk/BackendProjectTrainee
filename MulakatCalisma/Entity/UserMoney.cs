
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MulakatCalisma.Entity
{
    public class UserMoney
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "Decimal(18,2)")]
        public decimal Money { get; set; }
    }
}
