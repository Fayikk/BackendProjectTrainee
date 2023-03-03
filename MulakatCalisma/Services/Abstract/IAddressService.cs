using MulakatCalisma.DTO;
using MulakatCalisma.Entity;

namespace MulakatCalisma.Services.Abstract
{
    public interface IAddressService
    {
        Task<ServiceResponse<AddressDTO>> AddAddress(AddressDTO address);
    }
}
