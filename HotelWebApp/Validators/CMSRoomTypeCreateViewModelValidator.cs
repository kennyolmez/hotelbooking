using FluentValidation;
using HotelWebApp.ViewModels;

namespace HotelWebApp.Validators
{
    public class CMSRoomTypeCreateViewModelValidator : AbstractValidator<CMSRoomTypeCreateViewModel>
    {
        public CMSRoomTypeCreateViewModelValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .WithMessage("{PropertyName} input cannot be empty.");

            RuleFor(x => x.Price)
                .NotNull()
                .WithMessage("{PropertyName} input cannot be empty.");

            RuleFor(x => x.Description)
                .NotNull()
                .WithMessage("{PropertyName} input cannot be empty.");

            RuleFor(x => x.ImgUrl)
                .NotNull()
                .WithMessage("{PropertyName} input cannot be empty.");

        }
    }
}
