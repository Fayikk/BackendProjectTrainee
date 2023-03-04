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
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
                _basketService = basketService;
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> AddBasket(Basket basket)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (user == null)
            {
                return BadRequest("Login");
            }
            basket.UserId = int.Parse(user);
            var result = await _basketService.AddBasket(basket);
            return Ok(basket);  

        }

    }
}
