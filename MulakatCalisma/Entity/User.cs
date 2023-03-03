namespace MulakatCalisma.Entity
{
    public class User
    {
        public int Id { get; set; } 
        public string Email { get; set; } =string.Empty;
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Role { get; set; } = "Customer";
    }
}
