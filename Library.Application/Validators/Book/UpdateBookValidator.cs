using FluentValidation;
using Library.BusinessLogic.DTO.Book;

namespace Library.BusinessLogic.Validators.Book
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage("Book title is required")
               .MaximumLength(200).WithMessage("Book title cannot exceed 200 characters")
               .Matches(@"^[a-zA-Zа-яА-Я0-9\s\.,!?\-\'""\(\)]+$").WithMessage("Book title contains invalid characters");

            RuleFor(x => x.PublishedYear)
                .InclusiveBetween(1000, DateTime.Now.Year + 1)
                    .WithMessage($"Publication year must be between 1000 and {DateTime.Now.Year + 1}");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage("Author ID must be a positive number")
                .LessThan(100000).WithMessage("Author ID is too large");
        }
    }
}