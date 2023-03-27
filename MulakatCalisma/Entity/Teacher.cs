
using System.ComponentModel.DataAnnotations;

namespace MulakatCalisma.Entity
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; } 
        public int Userıd { get; set; }
        public string Url { get; set; } 
        public string PublicId { get; set; }    
    }
}
