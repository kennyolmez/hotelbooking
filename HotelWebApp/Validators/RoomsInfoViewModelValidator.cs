using FluentValidation;
using HotelWebApp.ViewModels;

namespace HotelWebApp.Validators
{
    public class RoomsInfoViewModelValidator : AbstractValidator<RoomsInfoViewModel>
    {
        public RoomsInfoViewModelValidator()
        {
            RuleFor(x => x.Description)
                .NotNull()
                .WithMessage("Must post a review.")
                .MinimumLength(10)
                .WithMessage("Review must exceed 10 characters.")
                .MaximumLength(1000)
                .WithMessage("Review cannot exceed 1000 characters.")
                .WithName("Review");

            RuleFor(x => x.Rating)
                .NotNull()
                .WithMessage("{PropertyName} input is required.")
                .Must(x => x >= 1 && x <= 5)
                .WithMessage("{PropertyName} must be between 1-5.");
                
        }
    }
}



