using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MulakatCalisma.DTO;
using MulakatCalisma.Entity;
using MulakatCalisma.Services.Abstract;
using System.Security.Claims;

namespace MulakatCalisma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<AddressDTO>>> AddAddress(AddressDTO address)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (user != null)
            {
                address.UserId = int.Parse(user);
               var result = _addressService.AddAddress(address);
                return Ok(result);
            }
            return BadRequest(address);
        }

        [HttpGet,Authorize]
        public async Task<ActionResult<ServiceResponse<AddressDTO>>> GetAddressByUser()
        {
            var result = await _addressService.GetByAddress();
            return Ok(result);  
        }
    }
}
