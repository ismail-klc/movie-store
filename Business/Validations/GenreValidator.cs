using Entities.Dtos;
using FluentValidation;

namespace Business.Validations
{
    public class GenreValidator: AbstractValidator<CreateGenreDto>
    {
        public GenreValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Lütfen isim alanını boş bırakmayınız!");
        }
    }
}