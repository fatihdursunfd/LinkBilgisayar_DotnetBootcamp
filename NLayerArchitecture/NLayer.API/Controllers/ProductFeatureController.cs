using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Data.Models;
using NLayer.Service.Dto;
using NLayer.Service.Interfaces;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductFeatureController : ControllerBase
    {
        private readonly IProductFeatureService productFeatureService;

        public ProductFeatureController(IProductFeatureService productFeatureService)
        {
            this.productFeatureService = productFeatureService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var pfs = productFeatureService.GetAll();
            return Ok(pfs);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var pf = productFeatureService.GetById(id);
            return Ok(pf);
        }

        [HttpPost]
        public IActionResult Add(ProductFeatureAddDto productFeature)
        {
            productFeatureService.Add(productFeature);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove(int id)
        {
            productFeatureService.Remove(id);
            return Ok();
        }
    }
}
