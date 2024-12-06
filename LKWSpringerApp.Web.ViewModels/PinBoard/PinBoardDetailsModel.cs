namespace LKWSpringerApp.Web.ViewModels.PinBoard
{
    public class PinBoardDetailsModel
    {
        public Guid DriverId { get; set; }
        public string? DrivingLicenseExpDate { get; set; }
        public string? DrivingCardExpDate { get; set; }
        public string? DrivingLicenseRenewalDate { get; set; }
        public string? DrivingCardRenewalDate { get; set; }
        public string? News { get; set; }
        public string? ImportantNews { get; set; }
        public string? UpcomingCourse { get; set; }
        public string? UpcomingCourseDate { get; set; }
    }
}
