using static LKWSpringerApp.Common.EntityValidationConstants.Tour;
using static LKWSpringerApp.Common.ErrorMessagesConstants.Tour;

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LKWSpringerApp.Web.ViewModels.Tour
{
    public class EditTourModel
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = TourNameErrorMessage)]
        [StringLength(TourNameMaxLength, MinimumLength = TourNameMinLength)]
        public string TourName { get; set; } = null!;
        
        [Required(ErrorMessage = TourNumberErrorMessage)]
        [Range(1, 1000, ErrorMessage = TourRangeNumberErrorMessage)]
        public int TourNumber { get; set; }
        public List<SelectListItem> Drivers { get; set; } = new List<SelectListItem>();
        public List<Guid> SelectedDriverIds { get; set; } = new List<Guid>();

        public List<DriverViewModel> AssociatedDrivers { get; set; } = new List<DriverViewModel>();
    }

    public class DriverViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
    }
}

