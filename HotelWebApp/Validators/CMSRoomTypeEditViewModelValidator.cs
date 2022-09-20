using FluentValidation;
using HotelWebApp.ViewModels;

namespace HotelWebApp.Validators
{
    public class CMSRoomTypeEditViewModelValidator : AbstractValidator<CMSRoomTypeEditViewModel>
    {
        public CMSRoomTypeEditViewModelValidator()
        {
            RuleForEach(x => x.Titles)
                .NotNull()
                .WithMessage("{PropertyName} input is required!");

            RuleForEach(x => x.Descriptions)
                .NotEmpty()
                .WithMessage("{PropertyName} input is required!");

            RuleForEach(x => x.ImgUrls)
                .NotEmpty()
                .WithMessage("{PropertyName} input is required!");

            // Won't work because decimal is not nullable so the default error will override this
            // Fix it
            RuleForEach(x => x.Prices)
                .NotEmpty()
                .WithMessage("{PropertyName} input is required!");
        }
    }
}

