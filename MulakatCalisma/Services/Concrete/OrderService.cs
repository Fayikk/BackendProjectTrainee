﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MulakatCalisma.Context;
using MulakatCalisma.DTO;
using MulakatCalisma.Entity;
using MulakatCalisma.Services.Abstract;

namespace MulakatCalisma.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public OrderService(ApplicationDbContext context, IProductService productService, IAuthService authService, IMapper mapper)
        {
            _context = context;
            _productService = productService;
            _authService = authService;
        }

        public async Task<ServiceResponse<Order>> CreateOrder(Order order)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == order.UserId);
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == order.ProductId);
            if (user == null && product == null)
            {
                return new ServiceResponse<Order>
                {
                    Success = false,
                    Message = "Fail Request",
                };
            }
            else if (user != null && product != null)
            {
                //var obj = _mapper.Map<OrderDTO, Order>(order);
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return new ServiceResponse<Order>
                {
                    Message = "Order Succesfully",
                    Success = true,
                };
            }
            return new ServiceResponse<Order>
            {
                Message = "ss",
            };
        }

        public async Task<ServiceResponse<List<Order>>> GetProductByUser(int userId)
        {
            var response = await _context.Orders.Where(x=>x.UserId== userId).ToListAsync(); 
            if (response != null)
            {
                return new ServiceResponse<List<Order>>
                {
                    Success = true,
                    Data = response,
                };

            }
            return new ServiceResponse<List<Order>> { Success = false, };

        }
    }
}
