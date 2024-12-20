﻿using static LKWSpringerApp.Common.EntityValidationConstants.Driver;
using static LKWSpringerApp.Common.ErrorMessagesConstants.Driver;
using LKWSpringerApp.Web.ViewModels.Tour;

using System.ComponentModel.DataAnnotations;

namespace LKWSpringerApp.Web.ViewModels.Driver
{
    [EitherSpringerOrStamm(ErrorMessage = DriverCanNotBeBoth)]
    public class AddDriverModel
    {
        [Required(ErrorMessage = DriverFirstNameErrorMessage)]
        [StringLength(DriverFirstNameMaxLength, MinimumLength = DriverFirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = DriverSecondNameErrorMessage)]
        [StringLength(DriverSecondNameMaxLength, MinimumLength = DriverSecondNameMinLength)]
        public string SecondName { get; set; } = null!;

        [Required(ErrorMessage = DriverBirthDateErrorMessage)]
        public string BirthDate { get; set; } = null!;

        [Required(ErrorMessage = DriverStartDateErrorMessage)]
        public string StartDate { get; set; } = null!;

        [Required(ErrorMessage = DriverPhoneNumberErrorMessage)]
        [RegularExpression(DriverPhoneNumberFormatPattern)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public bool Springerdriver { get; set; }

        [Required]
        public bool Stammdriver { get; set; }

        public List<TourViewModel> Tours { get; set; } = new List<TourViewModel>();
    }

    public class EitherSpringerOrStammAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (AddDriverModel)validationContext.ObjectInstance;
            if (model.Springerdriver && model.Stammdriver)
            {
                return new ValidationResult(DriverCanNotBeBoth);
            }
            return ValidationResult.Success;
        }
    }
}
