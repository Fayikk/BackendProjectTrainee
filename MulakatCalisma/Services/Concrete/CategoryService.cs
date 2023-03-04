using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MulakatCalisma.Context;
using MulakatCalisma.DTO;
using MulakatCalisma.Entity;
using MulakatCalisma.Services.Abstract;

namespace MulakatCalisma.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CategoryService(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<CategoryDTO>> AddCategory(CategoryDTO category)
        {
            var obj = _mapper.Map<CategoryDTO, Category>(category);
            var addedObj = _context.Categories.Add(obj);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<Category, CategoryDTO>(addedObj.Entity);
            return new ServiceResponse<CategoryDTO>
            {
                Data = category,
                Success = true,
                Message = "Add operation is successfully",
            };
    
        }

   
    }
}
