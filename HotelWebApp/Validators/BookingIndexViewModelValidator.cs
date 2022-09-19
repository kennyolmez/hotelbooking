using FluentValidation;
using HotelWebApp.ViewModels;

namespace HotelWebApp.Validators
{
    public class BookingIndexViewModelValidator : AbstractValidator<BookingIndexViewModel>
    {
        public BookingIndexViewModelValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .WithMessage("{PropertyName} input is required!")
                .Length(2, 25)
                .WithMessage("{PropertyName} length must be between 2-25!")
                .Matches(@"^[a-zA-Z-']*$")
                .WithMessage("{PropertyName} must contain only letters A-Z");

            RuleFor(x => x.LastName)
                .NotNull()
                .WithMessage("{PropertyName} input is required!")
                .Length(2, 25)
                .WithMessage("{PropertyName} length must be between 2-25!")
                .Matches(@"^[a-zA-Z-']*$")
                .WithMessage("{PropertyName} must contain only letters A-Z");

            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("{PropertyName} input is required!")
                .Length(5, 100)
                .WithMessage("{PropertyName} length must be between 5-100!")
                .EmailAddress()
                .WithMessage("{PropertyName} must be valid email address format");
        }

    }
}
