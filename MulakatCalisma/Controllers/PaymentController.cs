using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MulakatCalisma.Helper;

namespace MulakatCalisma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ISample _sample;
        public PaymentController(ISample sample)
        {
            _sample = sample;
        }

        //Sample sample = new Sample();
        //API api = new API();

        [HttpPost]
        [Authorize]
        public IActionResult Checkout()
        {
            var result = _sample.Should_Create_Payment();
            return Ok(result);
        }

        [HttpPost("Cancel")]
        [Authorize]
        public IActionResult Cancel()
        {
            var result = _sample.Cancel_Refund();
            return Ok(result);
        }
      

    }
}
