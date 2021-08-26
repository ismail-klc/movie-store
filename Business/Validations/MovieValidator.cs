using Entities.Dtos;
using FluentValidation;

namespace Business.Validations
{
    public class MovieValidator : AbstractValidator<CreateMovieDto>
    {
        public MovieValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Lütfen isim alanını boş bırakmayınız!");
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Lütfen geçerli bir fiyat giriniz!");
            RuleFor(x => x.Year)
                .GreaterThan(1000)
                .WithMessage("Lütfen geçerli bir yıl giriniz!");
            RuleFor(x => x.GenreId)
                .GreaterThan(0)
                .WithMessage("Lütfen geçerli bir Id giriniz!");
            RuleFor(x => x.DirectorId)
                .GreaterThan(0)
                .WithMessage("Lütfen geçerli bir Id giriniz!");
        }
    }
}