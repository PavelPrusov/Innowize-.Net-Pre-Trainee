using FluentValidation;
using Library.BusinessLogic.DTO.Book;
using Library.BusinessLogic.Resources;

namespace Library.BusinessLogic.Validators.Book
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ValidationMessages.FieldRequierd)
                .MaximumLength(200).WithMessage(string.Format(ValidationMessages.MaxLength, 200))
                .Matches(@"^[a-zA-Zа-яА-Я0-9\s\.,!?\-\'""\(\)]+$").WithMessage(ValidationMessages.InvalidNameFormat);

            RuleFor(x => x.PublishedYear)
                .InclusiveBetween(1000, DateTime.Now.Year + 1)
                .WithMessage($"Publication year must be between 1000 and {DateTime.Now.Year + 1}");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage(ValidationMessages.MustBePositive)
                .LessThan(100000).WithMessage(ValidationMessages.TooLarge);
        }
    }
}