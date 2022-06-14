using API.Model;
using FluentValidation;

namespace API.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.UserID).GreaterThan(0);
            RuleFor(x => x.Mail).NotEmpty();
            RuleFor(x => x.Birthday).NotEmpty();
            RuleFor(x => x.Age).GreaterThan(0);
        }
    }
}
