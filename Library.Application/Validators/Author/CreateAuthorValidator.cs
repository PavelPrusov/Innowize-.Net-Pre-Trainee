using FluentValidation;
using Library.BusinessLogic.DTO.Author;

namespace Library.BusinessLogic.Validators
{
    public class CreateAuthorValidator : AbstractValidator<CreateAuthorDto>
    {
        public CreateAuthorValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя автора обязательно")
                .MaximumLength(100).WithMessage("Имя автора не может превышать 100 символов")
                .Matches(@"^[a-zA-Zа-яА-Я\s\.\-]+$").WithMessage("Имя автора может содержать только буквы, пробелы, точки и дефисы");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateOnly.FromDateTime(DateTime.Now))
                    .WithMessage("Дата рождения должна быть в прошлом")
                .GreaterThanOrEqualTo(new DateOnly(1800, 1, 1))
                    .WithMessage("Дата рождения не может быть раньше 1800 года");
        }
    }
}