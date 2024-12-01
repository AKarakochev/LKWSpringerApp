using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LKWSpringerApp.Data.Models
{
    public class DriverTour
    {
        [Required]
        [Comment("Unique identifier.")]
        public Guid DriverId { get; set; }
        public Driver Driver { get; set; } = null!;
        
        [Required]
        [Comment("Unique identifier.")]
        public Guid TourId { get; set; }
        public Tour Tour { get; set; } = null!;
    }
}
