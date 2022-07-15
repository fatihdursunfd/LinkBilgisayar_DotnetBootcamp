using Assessment.Core.DTOs;
using Assessment.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Core.Services
{
    public interface ICustomerService
    {
        public Task<Customer> AddWithImage(Customer customer , IFormFile formFile);
        public Task<Customer> UpdateImage(int customerId , IFormFile formFile);
    }
}
