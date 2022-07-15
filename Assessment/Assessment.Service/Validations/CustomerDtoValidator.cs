using Assessment.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Service.Validations
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.City).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Telephone).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
