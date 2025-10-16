using FluentValidation;
using Library.BusinessLogic.DTO.Author;
using Library.BusinessLogic.Resources;

namespace Library.BusinessLogic.Validators.Author
{
    public class CreateAuthorValidator : AbstractValidator<CreateAuthorDto>
    {
        public CreateAuthorValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.FieldRequierd)
                .MaximumLength(100).WithMessage(string.Format(ValidationMessages.MaxLength, 100))
                .Matches(@"^[a-zA-Zа-яА-Я\s\.\-]+$").WithMessage(ValidationMessages.InvalidNameFormat);

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage(ValidationMessages.DateMustBePast)
                .GreaterThanOrEqualTo(new DateOnly(1800, 1, 1)).WithMessage(string.Format(ValidationMessages.DateMinValue, 1800));
        }
    }
}