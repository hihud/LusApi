using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LusApi.Model;
using LusCore.Product;
using LusService.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LusApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("GetAll")]
        [HttpGet]
        [Authorize]
        public IActionResult GetProduct(string message)
        {
            var result = _productService.GetAllProduct();
            return Ok(result);
        }
        [Route("GetProductInfor")]
        [HttpGet]
        [Authorize]
        public IActionResult GetProductInfor(string lusid)
        {
            var id = Guid.Parse(lusid);
            var result = _productService.GetProduct(id);
            return Ok(result);
        }

        [Route("AddProduct")]
        [HttpPost]
        [Authorize]
        public IActionResult AddProduct([FromBody] UpdateProductModel product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            var productDto = new ProductDto()
            {
                Category = product.Category,
                Collection = product.Collection,
                Description = product.Description,
                LusId = product.LusId,
                Name = product.Name,
                Provider = product.Provider,
                Type = product.Type
            };
            //_productService.AddProduct(productDto);
            return Ok();
        }

        [Route("UpdateProduct")]
        [HttpPut]
        [Authorize]
        public IActionResult UpdateProduct([FromBody] ProductDto product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            var result = _productService.UpdateProduct(product);
            return Ok(result);
        }
    }
}