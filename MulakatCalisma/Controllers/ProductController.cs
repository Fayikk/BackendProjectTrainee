using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MulakatCalisma.DTO;
using MulakatCalisma.Entity;
using MulakatCalisma.Services.Abstract;
using MulakatCalisma.Validations;

namespace MulakatCalisma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
                _productService = productService;
        }

        [HttpPost]
        //[Authorize(Roles ="Admin")]
        public async Task<ActionResult<ServiceResponse<ProductDTO>>> AddProduct(ProductDTO product)
        {
            ProductValidator validations = new ProductValidator();
            ValidationResult results = validations.Validate(product);
            string temp = "";
            string[] arr = new string[results.Errors.Count];
            if (results.IsValid)
            {
                var result = await _productService.CreateProduct(product);
                return Ok(result);
            }

            else
            {
                foreach (var item in results.Errors)
                {
                    return BadRequest(item.ErrorMessage);
                }
            }
            return null;
        }

        [HttpPost("{name}")]
        public async Task<ActionResult<ServiceResponse<ProductDTO>>> GetProduct([FromRoute]string name)
        {
            var result = await _productService.GetProduct(name);
            return Ok(result);

        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ProductDTO>>>> GetProducts()
        {
            var result = await _productService.GetAll();
            return Ok(result);
        }

        [HttpGet("ByCategory")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductByCategory(int categoryId)
        {
            var response = await _productService.GetProductByCategory(categoryId);
            if (response == null)
            {
                return BadRequest(categoryId);
            }
            return Ok(response);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetAll()
        {
            var result = await _productService.GetAll();
            return Ok(result);
        }


    }
}
