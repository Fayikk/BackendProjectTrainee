
using System.ComponentModel.DataAnnotations;

namespace MulakatCalisma.Entity
{
    public class Star
    {
        [Key]
        public int Id { get; set; } 
        public int UserId { get; set; } 
        public int ProductId { get; set; }  
        public string ProductName { get; set; }
    }
}
