using Entities.Dtos;
using FluentValidation;

namespace Business.Validations
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Lütfen geçerli bir email adresi giriniz!");
            RuleFor(x => x.Password)
                .MaximumLength(12)
                .MinimumLength(6)
                .WithMessage("Lütfen 6-12 karakterlik bir parola giriniz!");
        }
    }
}