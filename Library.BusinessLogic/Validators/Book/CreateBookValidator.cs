using FluentValidation;
using Library.BusinessLogic.DTO.Book;
using Library.BusinessLogic.Resources;

namespace Library.BusinessLogic.Validators.Book
{
    public class CreateBookValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(string.Format(ValidationMessages.FieldRequired, "Book title"))
                .MaximumLength(200).WithMessage(string.Format(ValidationMessages.MaxLength, "Book title", 200))
                .Matches(@"^[a-zA-Zа-яА-Я0-9\s\.,!?\-\'""\(\)]+$").WithMessage(string.Format(ValidationMessages.InvalidNameFormat, "Book title"));

            RuleFor(x => x.PublishedYear)
                .InclusiveBetween(1000, DateTime.Now.Year + 1)
                .WithMessage($"Publication year must be between 1000 and {DateTime.Now.Year + 1}");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage(string.Format(ValidationMessages.MustBePositive, "Author ID"))
                .LessThan(100000).WithMessage(string.Format(ValidationMessages.TooLarge, "Author ID"));
        }
    }
}