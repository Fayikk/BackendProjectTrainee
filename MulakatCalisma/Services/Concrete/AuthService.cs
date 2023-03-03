using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MulakatCalisma.Context;
using MulakatCalisma.Entity;
using MulakatCalisma.Entity.Model;
using MulakatCalisma.Services.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MulakatCalisma.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthService(ApplicationDbContext context, IConfiguration config, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _config = config;
            _contextAccessor = contextAccessor;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(int userId, string oldPassword, string newPassword, string ConfirmPassword)
        {
            var response = await _context.Users.FindAsync(userId);
            if (response == null)
            {
                new ServiceResponse<bool>
                {
                    Message = "User is not found",
                    Success = false,
                };
            }
            else
            {
                if (!VerifyPasswordHash(oldPassword, response.PasswordHash, response.PasswordSalt))
                {
                    new ServiceResponse<bool>
                    {
                        Message = "Your current password is not match",
                        Success=false
                    };
                }
                else
                {
                    CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
                    response.PasswordHash=passwordHash;
                    response.PasswordSalt=passwordSalt;
                    await _context.SaveChangesAsync();
                    return new ServiceResponse<bool> { Success = true };


                }
            }

            return new ServiceResponse<bool>
            {
                Message = "something gone doesn't went",
            };

        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
        }

        public string GetUserEmail()
        {
            return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }

        public int GetUserId()
        {
            return int.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (user == null)
            {
                response.Message = "User is not found";
                response.Success = false;
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Please Check Your Password";
            }
            else
            {
                response.Data = CreateToken(user);
                response.Message = "User Login Successfully";
            }

            return response;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Email),
                new Claim(ClaimTypes.Role,user.Role),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:SecretKey").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                 claims: claims,
                 expires: DateTime.Now.AddDays(1),
                 signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExists(user.Email))
            {
                return new ServiceResponse<int> { Success = false, Message = "User already esixt" };
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.DateCreated = DateTime.Now;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int> { Data = user.Id, Success = true, Message = "User Creation Successfully" };

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(x => x.Email.ToLower().Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }

        public async Task<ServiceResponse<bool>> RoleForAdmin(string email)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (result==null)
            {
                return new ServiceResponse<bool>
                {
                    Message = "Not Found This Email",
                    Success = false,
                };
            }
            else
            {
                result.Role = "Admin";
                await _context.SaveChangesAsync();
                return new ServiceResponse<bool>
                {
                    Success = false,
                };
            }

        }
    }
}
