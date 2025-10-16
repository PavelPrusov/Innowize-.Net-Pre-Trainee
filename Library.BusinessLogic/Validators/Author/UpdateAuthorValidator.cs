using FluentValidation;
using Library.BusinessLogic.DTO.Author;
using Library.BusinessLogic.Resources;

namespace Library.BusinessLogic.Validators.Author
{
    public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorDto>
    {
        public UpdateAuthorValidator()
        {
            RuleFor(x => x.Name)
                     .NotEmpty().WithMessage(string.Format(ValidationMessages.FieldRequired, "Author name"))
                     .MaximumLength(100).WithMessage(string.Format(ValidationMessages.MaxLength, "Author name", 100))
                     .Matches(@"^[a-zA-Zа-яА-Я\s\.\-]+$").WithMessage(string.Format(ValidationMessages.InvalidNameFormat, "Author name"));

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage(string.Format(ValidationMessages.DateMustBePast, "Date of birth"))
                .GreaterThanOrEqualTo(new DateOnly(1800, 1, 1)).WithMessage(string.Format(ValidationMessages.DateMinValue, "Date of birth", 1800));
        }
    }
}