using Microsoft.AspNetCore.Components.Forms;
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
        Task<ServiceResponse<List<Product>>> GetProductByCategory(int categoryId);
        Task<ServiceResponse<Product>> IncrementStar(int productId);
        Task<ServiceResponse<ProductSearchResult>> GetProducts(int page);
        Task<ServiceResponse<List<Product>>> SearchProducts(string searchText);

    }
}
