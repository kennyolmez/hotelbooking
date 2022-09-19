using FluentValidation;
using HotelWebApp.ViewModels;

namespace HotelWebApp.Validators
{
    public class HomeIndexViewModelValidator : AbstractValidator<HomeIndexViewModel>
    {
        public HomeIndexViewModelValidator()
        {
            // Either use the cascade stop function or chain the predicates such that they depend on the result from the one before.
            // So, for example, don't even check the LaterThan or NotToday predicates unless null check has been passed

            RuleFor(x => x.CheckInDate)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("{PropertyName} input must not be empty!")
                .Must((x, c) => LaterThan(x.CheckInDate, x.CheckOutDate))
                .WithMessage("{PropertyName} cannot be same date as or later than Check Out Date")
                .Must((x, c) => NotToday(x.CheckInDate))
                .WithMessage("Cannot book past dates");


            RuleFor(x => x.CheckOutDate)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("{PropertyName} input must not be empty!")
                .NotEqual(x => x.CheckInDate)
                .WithMessage("{PropertyName} cannot be the same date as Check Out Date")
                .Must((x, c) => MaximumAmountOfDays(x.CheckInDate, x.CheckOutDate))
                .WithMessage("Cannot book more than 30 days!")
                .Must((x, c) => NotToday(x.CheckOutDate))
                .WithMessage("Cannot book past dates");

            RuleFor(x => x.MinPrice)
                .Cascade(CascadeMode.Stop)
                .LessThan(x => x.MaxPrice)
                .WithMessage("Minimum price must be less than maximum price!")
                .WithName("Min Price")
                .Must(x => x >= 0 && x <= 1000)
                .WithMessage("{PropertyName} must be between 0 and 1000");

            RuleFor(x => x.MaxPrice)
                .Cascade(CascadeMode.Stop)
                .Must(x => x >= 0 && x <= 1000)
                .WithMessage("{PropertyName} must be between 0 and 1000")
                .WithName("Max Price");

        }

        private bool LaterThan(string checkInDate, string checkOutDate)
        {
            if (Convert.ToDateTime(checkInDate) <= Convert.ToDateTime(checkOutDate))
            {
                return true;
            }

            return false;
        }
        private bool MaximumAmountOfDays(string checkInDate, string checkOutDate)
        {
            if ((Convert.ToDateTime(checkOutDate) - Convert.ToDateTime(checkInDate)).TotalDays <= 30)
            {
                return true;
            }

            return false;
        }

        private bool NotToday(string date)
        {
            if(Convert.ToDateTime(date) >= DateTime.Today)
            {
                return true;
            }

            return false;
        }
    }
}



