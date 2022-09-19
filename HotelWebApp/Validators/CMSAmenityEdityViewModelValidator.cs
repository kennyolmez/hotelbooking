using FluentValidation;
using HotelWebApp.ViewModels;

namespace HotelWebApp.Validators
{
    public class CMSAmenityEdityViewModelValidator : AbstractValidator<CMSAmenityEditViewModel>
    {
        public CMSAmenityEdityViewModelValidator()
        {
            RuleForEach(x => x.Names)
                .NotNull()
                .WithMessage("{PropertyName} input is required!")
                .Length(2, 25)
                .WithMessage("{PropertyName} length must be between 2-25 characters!")
                .Matches(@"^[a-zA-Z-']*$")
                .WithMessage("{PropertyName} must contain only letters A-Z")
                .WithName("Name");

            RuleForEach(x => x.Icons)
                .NotEmpty()
                .WithMessage("{PropertyName} input is required!")
                .WithName("Icon");
        }
    }
}
