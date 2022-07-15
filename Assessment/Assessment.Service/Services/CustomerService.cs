using Assessment.Core.Models;
using Assessment.Core.Repositories;
using Assessment.Core.Services;
using Assessment.Core.UnitOfWorks;
using Assessment.RabbitMQ;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Service.Services
{
    public class CustomerService : Service<Customer>, ICustomerService
    {
        private readonly RabbitMQPublisher _rabbitMQPublisher;

        public CustomerService(IUnitOfWork unitOfWork, IGenericRepo<Customer> genericRepo, RabbitMQPublisher rabbitMQPublisher) : base(unitOfWork, genericRepo)
        {
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public async Task<Customer> AddWithImage(Customer customer, IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                var randomImageName = Guid.NewGuid() + Path.GetExtension(formFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", randomImageName);

                await using FileStream stream = new(path, FileMode.Create);
                await formFile.CopyToAsync(stream);

                customer.ImageName = randomImageName;
                customer.FirstName = customer.FirstName.ToUpper();
                customer.LastName = customer.LastName.ToUpper();
                customer.CreatedDate = DateTime.Now;

                _rabbitMQPublisher.Publish(new CustomerImageCreatedEvent() { ImageName = randomImageName });

                await AddAsync(customer);
            }
            return customer;
        }

        public async Task<Customer> UpdateImage(int customerId, IFormFile formFile)
        {
            var customer = await GetByIdAsync(customerId);

            if (formFile.Length > 0)
            {
                var randomImageName = Guid.NewGuid() + Path.GetExtension(formFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", randomImageName);

                await using FileStream stream = new(path, FileMode.Create);
                await formFile.CopyToAsync(stream);

                customer.ImageName = randomImageName;
                customer.UpdatedDate = DateTime.Now;

                _rabbitMQPublisher.Publish(new CustomerImageCreatedEvent() { ImageName = randomImageName });

                await SaveChangesAsync();
            }
            return customer;
        }
    }
}
