using Assessment.Core.Models;
using Assessment.Data;
using Assessment.Reporting.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Reporting.Services
{
    public class CustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public List<CustomerDto> GetCustomers()
        {
            var customers = _context.Customers.Include(x => x.CommercialTransactions)
                                              .Select(x => new CustomerDto()
                                              {
                                                  Fullname = x.FirstName + " " + x.LastName,
                                                  Email = x.Email,
                                                  City = x.City,
                                                  Count = x.CommercialTransactions.Where(x => x.JobDate < DateTime.Now && x.JobDate > DateTime.Now.AddDays(-7)).Count(),
                                                  TotalPrice = x.CommercialTransactions.Select(x => x.Price).Sum()
                                              })
                                              .OrderByDescending(x => x.Count)
                                              .ThenByDescending(x => x.TotalPrice)
                                              .Take(5)
                                              .ToList();

            return customers;
        }

        public List<CustomerCityDto> GetCustomersWithCity()
        {
            var customers = _context.Customers.GroupBy(x => x.City)
                                              .Select(x => new CustomerCityDto()
                                              {
                                                  City = x.Key,
                                                  CustomerCount = x.Count()
                                              })
                                              .OrderByDescending(x => x.CustomerCount)
                                              .ToList();
            return customers;
        }
        
    }
}
