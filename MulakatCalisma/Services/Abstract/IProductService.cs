using MulakatCalisma.DTO;
using MulakatCalisma.Entity;

namespace MulakatCalisma.Services.Abstract
{
    public interface IProductService
    {
        Task<ServiceResponse<ProductDTO>> CreateProduct(ProductDTO product);
        Task<ServiceResponse<int>> DeleteProduct(ProductDTO product);
        Task<ServiceResponse<ProductDTO>> UpdateProduct(ProductDTO product);
        Task<ServiceResponse<IEnumerable<ProductDTO>>> GetAll();
        Task<ServiceResponse<ProductDTO>> GetProduct(string Name);
        Task<ServiceResponse<ProductDTO>> AddImage(IFormFile file, string id);

    }
}
