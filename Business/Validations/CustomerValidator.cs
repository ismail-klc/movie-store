using Entities.Dtos;
using FluentValidation;

namespace Business.Validations
{
    public class CustomerValidator : AbstractValidator<CreateCustomerDto>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Firstname)
               .NotEmpty()
               .WithMessage("Lütfen isim alanını boş bırakmayınız!");
            RuleFor(x => x.Lastname)
                .NotEmpty()
                .WithMessage("Lütfen soyisim alanını boş bırakmayınız!");
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