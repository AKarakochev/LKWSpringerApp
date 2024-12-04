using static LKWSpringerApp.Common.EntityValidationConstants.PinBoard;

using System.ComponentModel.DataAnnotations;

namespace LKWSpringerApp.Web.ViewModels.PinBoard
{
    public class PinBoardEditDriverModel
    {
        public Guid DriverId { get; set; }

        [Required]
        [Display(Name = "Driving License Expiration Date")]
        public string DrivingLicenseExpDate { get; set; } = DateTime.Now.ToString(PinBoardDrivingLicenseExpDateFormat);

        [Required]
        [Display(Name = "Driving Card Expiration Date")]
        public string DrivingCardExpDate { get; set; } = DateTime.Now.ToString(PinBoardDrivingCardExpDateFormat);

        [Display(Name = "Driving License Renewal Date")]
        public string? DrivingLicenseRenewalDate { get; set; }

        [Display(Name = "Driving Card Renewal Date")]
        public string? DrivingCardRenewalDate { get; set; }

        [Display(Name = "Upcoming Course")]
        [StringLength(100)]
        public string? UpcomingCourse { get; set; }

        [Display(Name = "Upcoming Course Date")]
        public string? UpcomingCourseDate { get; set; }
    }
}
