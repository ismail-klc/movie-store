using Entities.Dtos;
using FluentValidation;

namespace Business.Validations
{
    public class ActorValidator : AbstractValidator<CreateActorDto>
    {
        public ActorValidator()
        {
            RuleFor(x => x.Firstname)
               .NotEmpty()
               .WithMessage("Lütfen isim alanını boş bırakmayınız!");
            RuleFor(x => x.Lastname)
                .NotEmpty()
                .WithMessage("Lütfen soyisim alanını boş bırakmayınız!");
        }
    }
}