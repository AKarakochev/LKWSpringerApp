using static LKWSpringerApp.Common.EntityValidationConstants.Driver;
using static LKWSpringerApp.Common.ErrorMessagesConstants.Driver;
using LKWSpringerApp.Web.ViewModels.Tour;

using System.ComponentModel.DataAnnotations;

namespace LKWSpringerApp.Web.ViewModels.Driver
{
    public class EditDriverModel
    {
        public Guid Id { get; set; }

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

        public List<Guid> SelectedTourIds { get; set; } = new List<Guid>();
    }
}

