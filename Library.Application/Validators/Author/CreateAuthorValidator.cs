using FluentValidation;
using Library.BusinessLogic.DTO.Author;

namespace Library.BusinessLogic.Validators.Author
{
    public class CreateAuthorValidator : AbstractValidator<CreateAuthorDto>
    {
        public CreateAuthorValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("Author name is required")
              .MaximumLength(100).WithMessage("Author name cannot exceed 100 characters")
              .Matches(@"^[a-zA-Zа-яА-Я\s\.\-]+$").WithMessage("Author name can only contain letters, spaces, dots and hyphens");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Date of birth must be in the past")
                .GreaterThanOrEqualTo(new DateOnly(1800, 1, 1)).WithMessage("Date of birth cannot be earlier than 1800");
        }
    }
}