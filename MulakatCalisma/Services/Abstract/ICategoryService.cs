using MulakatCalisma.DTO;
using MulakatCalisma.Entity;

namespace MulakatCalisma.Services.Abstract
{
    public interface ICategoryService
    {
        Task<ServiceResponse<CategoryDTO>> AddCategory(CategoryDTO category);
    }
}
