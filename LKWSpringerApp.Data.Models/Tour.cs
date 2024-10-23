using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static LKWSpringerApp.Common.ErrorMessagesConstants.Tour;
using static LKWSpringerApp.Common.EntityValidationConstants.Tour;

namespace LKWSpringerApp.Data.Models
{
    public class Tour
    {
        public Tour()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Unique identifier.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = TourNumberErrorMessage)]
        [Comment("The number of the tour.")]
        [Range(1, 1000,ErrorMessage = TourRangeNumberErrorMessage)]
        public int TourNumber { get; set; }

        [Required(ErrorMessage = TourNameErrorMessage)]
        [Comment("The name of the tour.")]
        [MaxLength(TourNameMaxLength)]
        public string TourName { get; set; } = null!;

        public bool IsDeleted { get; set; }

        [Required]
        public Guid DriverId { get; set; }

        [ForeignKey(nameof(DriverId))]
        public Driver Driver { get; set; } = null!;

        public ICollection<TourClient> ToursClients { get; set; } = new HashSet<TourClient>();
    }
}
