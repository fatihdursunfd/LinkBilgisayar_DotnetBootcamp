using Assessment.Core.DTOs;
using Assessment.Core.Models;
using Assessment.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.API.Controllers
{
    [Authorize]
    public class CustomerController : CustomBaseController
    {
        private readonly IService<Customer> _service;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(IService<Customer> service, IMapper mapper, ICustomerService customerService)
        {
            _service = service;
            _mapper = mapper;
            _customerService = customerService;
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromQuery]CustomerDto customerDto, IFormFile formFile)
        {
            var customer = await _customerService.AddWithImage(_mapper.Map<Customer>(customerDto),formFile);
            customerDto = _mapper.Map<CustomerDto>(customer);
            return CreateActionResult(CustomResponseDto<CustomerDto>.Success(200, customerDto));
        }


        [HttpPost("UpdateImage")]
        public async Task<IActionResult> UpdateImage(int customerId, IFormFile formFile)
        {
            var customer = await _customerService.UpdateImage(customerId,formFile);
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return CreateActionResult(CustomResponseDto<CustomerDto>.Success(200, customerDto));
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _service.GetAllAsync(); ;
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return CreateActionResult(CustomResponseDto<IEnumerable<CustomerDto>>.Success(200, customersDto));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _service.GetByIdAsync(id);
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return CreateActionResult(CustomResponseDto<CustomerDto>.Success(200, customerDto));
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveById(int id)
        {
            var customer = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(customer);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }


        [HttpPut]
        public async Task<IActionResult> Update(Customer customer)
        {
            var customerUpdated = await _service.UpdateAsync(customer);
            var customerDto = _mapper.Map<CustomerDto>(customerUpdated);
            return CreateActionResult(CustomResponseDto<CustomerDto>.Success(200, customerDto));
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoveAll()
        {
            var customers = await _service.GetAllAsync();
            await _service.RemoveAllAsync(customers);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
