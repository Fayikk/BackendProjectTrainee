using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MulakatCalisma.Context;
using MulakatCalisma.DTO;
using MulakatCalisma.Entity;
using MulakatCalisma.Entity.Model;
using MulakatCalisma.Services.Abstract;

namespace MulakatCalisma.Services.Concrete
{
    public class AddressService : IAddressService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public AddressService(ApplicationDbContext context, IMapper mapper, IAuthService authService)
        {
            _mapper = mapper;
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<AddressDTO>> AddAddress(AddressDTO address)
        {

            var result = _context.Addresses.FirstOrDefaultAsync(x => x.UserId == address.UserId);
            if (result != null)
            {
                return new ServiceResponse<AddressDTO>
                {
                    Success = false,
                 
                    Message = "Address Already Exist,if you want change your address,you must check update",
                };
            }
            else
            {
                var obj = _mapper.Map<AddressDTO, Address>(address);
                var addedObj = _context.Addresses.Add(obj);
                await _context.SaveChangesAsync();
                var response = _mapper.Map<Address, AddressDTO>(addedObj.Entity);
                return new ServiceResponse<AddressDTO>
                {
                    Data = response,
                    Success = true,
                    Message = "Address added",
                };
            }

        }

        public async Task<ServiceResponse<AddressDTO>> GetByAddress()
        {
            var result = await _context.Addresses.FirstOrDefaultAsync(x => x.Id == _authService.GetUserId());
            var response = _mapper.Map<Address, AddressDTO>(result);
            if (result != null)
            {
                return new ServiceResponse<AddressDTO>
                {
                    Data = response,
                    Success = true,
                    Message = response.FirstName,
                };
            }
            return new ServiceResponse<AddressDTO>
            {
                Success = false,
                Message = "You dont have address the system",
            };
        }
    }
}
