using API.Dto;
using FluentValidation;

namespace API.Validations
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Mail).NotEmpty();
            RuleFor(x => x.Birthday).NotEmpty();
        }
    }
}
