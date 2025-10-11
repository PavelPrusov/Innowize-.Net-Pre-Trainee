using FluentValidation;
using Library.BusinessLogic.DTO.Book;

namespace Library.BusinessLogic.Validators.Book
{
    public class CreateBookValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Название книги обязательно")
                .MaximumLength(200).WithMessage("Название книги не может превышать 200 символов")
                .Matches(@"^[a-zA-Zа-яА-Я0-9\s\.,!?\-\'""\(\)]+$")
                    .WithMessage("Название книги содержит запрещенные символы");

            RuleFor(x => x.PublishedYear)
                .InclusiveBetween(1000, DateTime.Now.Year + 1)
                    .WithMessage($"Год публикации должен быть между 1000 и {DateTime.Now.Year + 1}");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage("ID автора должен быть положительным числом");
        }
    }
}