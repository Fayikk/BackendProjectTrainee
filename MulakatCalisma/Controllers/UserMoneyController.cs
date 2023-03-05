using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MulakatCalisma.Entity;
using MulakatCalisma.Services.Abstract;
using System.Security.Claims;

namespace MulakatCalisma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMoneyController : ControllerBase
    {
        private readonly IUserMoneyService _userMoneyService;
        public UserMoneyController(IUserMoneyService userMoneyService)
        {
            _userMoneyService = userMoneyService;
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<ServiceResponse<UserMoney>>> AddMoneyYourAccount([FromBody] UserMoney account)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (user != null)
            {
                account.UserId = int.Parse(user);
                var response = await _userMoneyService.AddMoney(account);
                return Ok(response);

            }

            return BadRequest("Something went wrong");
        }
    }
}
