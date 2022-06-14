using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Service.Dto;
using NLayer.Service.Interfaces;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var category = _categoryService.GetById(id);
            return Ok(category);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("GetCounts")]
        public IActionResult GetCategoryCounts()
        {
            var categories = _categoryService.GetCategoryCounts();
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult Add(CategoryAddDto category)
        {
            _categoryService.Add(category);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove(int id)
        {
            _categoryService.Remove(id);
            return Ok();
        }


    }
}
