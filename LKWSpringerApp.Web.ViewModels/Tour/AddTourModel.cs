using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using static LKWSpringerApp.Common.EntityValidationConstants.Tour;
using static LKWSpringerApp.Common.ErrorMessagesConstants.Tour;


namespace LKWSpringerApp.Web.ViewModels.TourModels
{
    public class AddTourModel
    {
        [Required(ErrorMessage = TourNumberErrorMessage)]
        [Range(1, 1000, ErrorMessage = TourRangeNumberErrorMessage)]
        public int TourNumber { get; set; }

        [Required(ErrorMessage = TourNameErrorMessage)]
        [MaxLength(TourNameMaxLength)]
        public string TourName { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
