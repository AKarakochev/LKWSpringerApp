using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

using static LKWSpringerApp.Common.EntityValidationConstants.Tour;
using static LKWSpringerApp.Common.ErrorMessagesConstants.Tour;


namespace LKWSpringerApp.Web.ViewModels.Tour
{
    public class EditTourModel
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = TourNameErrorMessage)]
        [StringLength(TourNameMaxLength, MinimumLength = TourNameMinLength)]
        public string TourName { get; set; }
        
        [Required(ErrorMessage = TourNumberErrorMessage)]
        [Range(1, 1000, ErrorMessage = TourRangeNumberErrorMessage)]
        public int TourNumber { get; set; }
        public List<Guid> SelectedDriverIds { get; set; } = new List<Guid>();
        public List<SelectListItem> Drivers { get; set; } = new List<SelectListItem>();
    }
}
