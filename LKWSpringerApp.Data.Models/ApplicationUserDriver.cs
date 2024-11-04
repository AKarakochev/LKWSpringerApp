using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKWSpringerApp.Data.Models
{
    //[PrimaryKey(nameof(ApplicationUserId), nameof(DriverId))]
    public class ApplicationUserDriver
    {
        [Required]
        [Comment("Unique identifier.")]
        public Guid ApplicationUserId { get; set; }

        //[ForeignKey(nameof(ApplicationUserId))]
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
        
        [Required]
        [Comment("Unique identifier.")]
        public Guid DriverId { get; set; }
        //[ForeignKey(nameof(DriverId))]
        public virtual Driver Driver { get; set; } = null!;
    }
}
