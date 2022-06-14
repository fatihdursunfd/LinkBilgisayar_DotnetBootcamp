using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Service.Dto;
using NLayer.Service.Interfaces;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FullProductController : ControllerBase
    {
        private readonly IFullProductService _fullProductService;

        public FullProductController(IFullProductService fullProductService)
        {
            _fullProductService = fullProductService;
        }


        [HttpPost]
        public IActionResult AddFullProduct(FullProductAddDto fullProduct)
        {
            _fullProductService.Add(fullProduct);
            return Ok(); 
        }
    }
}
