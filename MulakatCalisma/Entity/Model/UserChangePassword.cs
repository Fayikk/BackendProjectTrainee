using System.ComponentModel.DataAnnotations;

namespace MulakatCalisma.Entity.Model
{
    public class UserChangePassword
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword",ErrorMessage ="is not match to with new password")]
        public string ConfirmPassword { get; set; } 
    }
}
