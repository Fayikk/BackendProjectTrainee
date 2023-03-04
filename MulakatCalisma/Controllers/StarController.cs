using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MulakatCalisma.Entity;
using MulakatCalisma.Services.Abstract;

namespace MulakatCalisma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarController : ControllerBase
    {
        private readonly IStarService _starService;
        public StarController(IStarService starService)
        {
            _starService = starService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> GiveStarProduct(Star star)
        {
            var response = await _starService.GiveStar(star);
            return Ok(response);
        }
    }
}
