using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MulakatCalisma.Context;
using MulakatCalisma.DTO;
using MulakatCalisma.Entity;
using MulakatCalisma.Services.Abstract;

namespace MulakatCalisma.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ProductDTO>> CreateProduct(ProductDTO product)
        {
            var result = await _context.Products.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(product.Name.ToLower()));
            if (result == null)
            {
                var obj = _mapper.Map<ProductDTO, Product>(product);
                obj.CreatedDate = DateTime.Now;
                var addedObj = _context.Products.Add(obj);
                await _context.SaveChangesAsync();

                var response = _mapper.Map<Product, ProductDTO>(addedObj.Entity);
                return new ServiceResponse<ProductDTO>
                {
                    Data = response,
                    Message = "Product is creation",
                    Success = true,
                };
            }
            else
            {
                return new ServiceResponse<ProductDTO>
                {
                    Success = false,
                    Message = "Item already exist,only you can change item's props",
                };
            }
        }

        public async Task<ServiceResponse<int>> DeleteProduct(ProductDTO product)
        {
            var result = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            //_mapper.Map<Product, ProductDTO>(result);
            if (result == null)
            {
                return new ServiceResponse<int>
                {
                    Message = "Item is not found",
                    Success = false,
                };
            }
            else
            {
                _context.Products.Remove(result);
                await _context.SaveChangesAsync();
                return new ServiceResponse<int>
                {
                    Data = product.Id,
                    Message = "Object is deleted",
                    Success = true
                };
            }

        }


        public async Task<ServiceResponse<ProductDTO>> GetProduct(string Name)
        {
            var result = await _context.Products.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(Name.ToLower()));
            var response = _mapper.Map<Product, ProductDTO>(result);
            if (response == null)
            {
                return new ServiceResponse<ProductDTO>
                {
                    Success = false,
                    Message = "This keyword dont match products",
                };

            }
            return new ServiceResponse<ProductDTO>
            {
                Data = response,
            };

        }

        public async Task<ServiceResponse<ProductDTO>> UpdateProduct(ProductDTO product)
        {

            var obj = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            if (obj == null)
            {
                return new ServiceResponse<ProductDTO>
                {
                    Message = "Item is not found",
                    Data = product,
                };
            }
            else
            {
                obj.Name = product.Name;
                obj.Description = product.Description;
                await _context.SaveChangesAsync();
                return new ServiceResponse<ProductDTO>
                {
                    Data = product,
                };
            }
        }

        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> GetAll()
        {
            var response = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_context.Products);

            return new ServiceResponse<IEnumerable<ProductDTO>>
            {
                Data = response,
                Success = true,
            };

        }

        public async Task<ServiceResponse<List<Product>>> GetProductByCategory(int categoryId)
        {
            var response = await _context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
            if (response == null)
            {
                return new ServiceResponse<List<Product>>
                {
                    Message = "Product is not found this category",
                    Success = false,
                };
            }
            return new ServiceResponse<List<Product>>
            {
                Success = true,
                Data = response,
            };
        }

        public async Task<ServiceResponse<Product>> IncrementStar(int productId)
        {
            var response = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (response == null)
            {
                return new ServiceResponse<Product>
                {
                    Message = "Product is not found",
                    Success = false,
                };
            }
            response.Like += 1;
            _context.Products.Update(response);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Product> { Success = true, Data = response };

        }

        public async Task<ServiceResponse<List<Product>>> GetProducts()
        {

            var response = await _context.Products.ToListAsync();

            if (response == null)
            {
                return new ServiceResponse<List<Product>>
                {
                    Success = false,
                    Message = "Product is not found",
                };
            }
            else
            {
                return new ServiceResponse<List<Product>>
                {
                    Success = true,
                    Data = response,
                };
            }
        }
    }
}
