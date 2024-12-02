using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using static LKWSpringerApp.Common.ErrorMessagesConstants.PinBoard;
using static LKWSpringerApp.Common.EntityValidationConstants.PinBoard;


namespace LKWSpringerApp.Data.Models
{
    public class PinBoard
    {
        public PinBoard()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Unique identifier.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = PinBoardDrivingLicenseExpDateErrorMessage)]
        [Comment("The expiration date of the driving license in MM/YYYY format.")]
        [MaxLength(PinBoardDrivingLicenseExpDateMaxLength)]
        public string DrivingLicenseExpDate { get; set; } = null!;

        [Required(ErrorMessage = PinBoardDrivingCardExpDateErrorMessage)]
        [Comment("The expiration date of the driving card in MM/YYYY format.")]
        [MaxLength(PinBoardDrivingCardExpDateMaxLength)]
        public string DrivingCardExpDate { get; set; } = null!;

        [Comment("The renewal date of the driving license in MM/YYYY format.")]
        [MaxLength(PinBoardDrivingLicenseRenewalDateMaxLength)]
        public string? DrivingLicenseRenewalDate { get; set; }

        [Comment("The renewal date of the driving card in MM/YYYY format.")]
        [MaxLength(PinBoardDrivingCardRenewalDateMaxLength)]
        public string? DrivingCardRenewalDate { get; set; }

        [Comment("Details of the upcoming course.")]
        [MaxLength(PinBoardUpcomingCourseMaxLength)]
        public string? UpcomingCourse { get; set; }

        [Comment("The date of the upcoming course in dd/MM/YYYY format.")]
        [DisplayFormat(DataFormatString = PinBoardUpcomingCourseDateFormat, ApplyFormatInEditMode = true)]
        public DateTime? UpcomingCourseDate { get; set; }

        [Comment("General news for the driver.")]
        [MaxLength(PinBoardNewsMaxLength)]
        public string? News { get; set; }

        [Comment("Important news for the driver.")]
        [MaxLength(PinBoardImportantNewsMaxLength)]
        public string? ImportantNews { get; set; }

        [Required]
        public Guid DriverId { get; set; }

        [ForeignKey(nameof(DriverId))]
        public Driver Driver { get; set; } = null!;
    }
}
