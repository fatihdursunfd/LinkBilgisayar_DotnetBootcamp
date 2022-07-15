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
    public class CommercialTransactionController : CustomBaseController
    {
        private readonly IService<CommercialTransaction> _service;
        private readonly IMapper _mapper;

        public CommercialTransactionController(IService<CommercialTransaction> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CommercialTransactionDto commercialTransactionDto)
        {
            var commercialTransaction = _mapper.Map<CommercialTransaction>(commercialTransactionDto);
            commercialTransaction.CreatedDate = DateTime.Now;
            commercialTransaction = await _service.AddAsync(commercialTransaction);
            commercialTransactionDto = _mapper.Map<CommercialTransactionDto>(commercialTransaction);
            return CreateActionResult(CustomResponseDto<CommercialTransactionDto>.Success(200, commercialTransactionDto));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var commercialTransactions = await _service.GetAllAsync();
            var commercialTransactionsDto = _mapper.Map<IEnumerable<CommercialTransactionDto>>(commercialTransactions);
            return CreateActionResult(CustomResponseDto<IEnumerable<CommercialTransactionDto>>.Success(200, commercialTransactionsDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var commercialTransaction = await _service.GetByIdAsync(id);
            var commercialTransactionDto = _mapper.Map<CommercialTransactionDto>(commercialTransaction);
            return CreateActionResult(CustomResponseDto<CommercialTransactionDto>.Success(200, commercialTransactionDto));
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoveById(int id)
        {
            var commercialTransaction = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(commercialTransaction);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CommercialTransaction commercialTransaction)
        {
            var commercialTransactionUpdated = await _service.UpdateAsync(commercialTransaction);
            var commercialTransactionDto = _mapper.Map<CommercialTransactionDto>(commercialTransactionUpdated);
            return CreateActionResult(CustomResponseDto<CommercialTransactionDto>.Success(200, commercialTransactionDto));
        }

    }
}
