using API.Dto;
using FluentValidation;

namespace API.Validations
{
    public class UserPostValidator : AbstractValidator<UserPostDto>
    {
        public UserPostValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Mail).NotEmpty();
            RuleFor(x => x.Birthday).NotEmpty();
        }
    }
}
