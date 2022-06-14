using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Data.Models;
using NLayer.Service.Dto;
using NLayer.Service.Interfaces;
using NLayer.Service.Services;

namespace NLayer.API.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response =  _productService.GetAll();
            return Ok(response);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var products = _productService.GetById(id);
            return Ok(products);
        }

        [HttpGet("GetFull")]
        public IActionResult GetProductFullModels(int categoryId)
        {
            // data come from store procedure

            var productFull = _productService.GetFull(categoryId);
            return Ok(productFull);
        }

        [HttpGet("GetFeature")]
        public IActionResult GetProductWithFeature()
        {
            // data come from view

            var productFeature = _productService.GetProductWithFeature();
            return Ok(productFeature);
        }

        [HttpPost]
        public IActionResult Add(ProductAddDto product)
        {
            _productService.Add(product);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove(int id)
        {
            _productService.Remove(id);
            return Ok();
        }
    }
}
