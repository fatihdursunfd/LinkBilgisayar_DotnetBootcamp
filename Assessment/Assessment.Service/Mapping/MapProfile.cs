using Assessment.Core.DTOs;
using Assessment.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<IdentityUser, UserDto>().ReverseMap();

            CreateMap<CommercialTransaction, CommercialTransactionDto>().ReverseMap();
        }
    }
}
