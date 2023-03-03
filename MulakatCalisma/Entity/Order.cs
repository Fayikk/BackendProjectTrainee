using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MulakatCalisma.Entity
{

    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; } 
        public int ProductId { get; set; }  
    }
}
