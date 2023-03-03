using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MulakatCalisma.Entity;
using MulakatCalisma.Entity.Model;
using MulakatCalisma.Services.Abstract;
using System.Security.Claims;

namespace MulakatCalisma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService; 
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegister user)
        {
            var response = await _authService.Register(new User { Email=user.Email },user.Password);
           
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin user)
        {
            var result = await _authService.Login(user.Email,user.Password);
            return Ok(result);
        }


        [HttpPut("change-password"),Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword(UserChangePassword user)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _authService.ChangePassword(int.Parse(userId),user.CurrentPassword,user.NewPassword, user.ConfirmPassword);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);    
        }

        [HttpPut("change-role"),Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> RoleForAdmin(string email)
        {
            var userId = User.FindFirstValue(ClaimTypes.Role);
            if (userId!="Admin")
            {
                return BadRequest("Yetkisiz Erişim");
            }
            else
            {
                var response = await _authService.RoleForAdmin(email);
                return Ok(response + " "+ email +" Admin");
            }

        }
    }
}
