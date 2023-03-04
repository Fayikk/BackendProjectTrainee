using Microsoft.EntityFrameworkCore;
using MulakatCalisma.Context;
using MulakatCalisma.Entity;
using MulakatCalisma.Services.Abstract;
using System.Reflection.Metadata.Ecma335;

namespace MulakatCalisma.Services.Concrete
{
    public class StarService : IStarService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;
        private readonly IProductService _productService;
        public StarService(ApplicationDbContext context, IAuthService authService, IProductService productService)
        {
            _context = context;
            _authService = authService;
            _productService = productService;
        }

        public async Task<ServiceResponse<bool>> GiveStar(Star star)
        {
            var response = await _context.Products.FirstOrDefaultAsync(x => x.Id == star.ProductId);
            var product = await _context.Stars.FirstOrDefaultAsync(x => x.ProductId == star.Id);
            var user = _authService.GetUserId();
            var ForUser = await _context.Stars.Where(x => x.UserId.Equals(user)).ToListAsync();

            foreach (var item in ForUser)
            {
                if (item.ProductId == star.ProductId)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message="You re exist starred this product",
                    };
                }
            }

            if (response == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Something went wrong",
                };
            }
           
                star.ProductId = response.Id;
                star.UserId = user;
                star.ProductName = response.Name;
                _context.Stars.Add(star);
                await _productService.IncrementStar(star.ProductId);

                await _context.SaveChangesAsync();
                return new ServiceResponse<bool> { Success = true };
            
        }
    }
}
//public int UserId { get; set; }
//public int ProductId { get; set; }
//public string ProductName { get; set; }