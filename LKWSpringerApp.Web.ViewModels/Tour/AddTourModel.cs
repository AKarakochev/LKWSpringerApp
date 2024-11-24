using static LKWSpringerApp.Common.EntityValidationConstants.Tour;
using static LKWSpringerApp.Common.ErrorMessagesConstants.Tour;

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LKWSpringerApp.Web.ViewModels.TourModels
{
    public class AddTourModel
    {
        [Required(ErrorMessage = TourNumberErrorMessage)]
        [Range(1, 1000, ErrorMessage = TourRangeNumberErrorMessage)]
        public int TourNumber { get; set; }

        [Required(ErrorMessage = TourNameErrorMessage)]
        [StringLength(TourNameMaxLength,MinimumLength =TourNameMinLength)]
        public string TourName { get; set; } = null!;

        public List<Guid> SelectedDriverIds { get; set; } = new List<Guid>();

        public List<SelectListItem> Drivers { get; set; } = new List<SelectListItem>();
    }
}
