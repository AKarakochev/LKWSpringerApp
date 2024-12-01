using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static LKWSpringerApp.Common.ErrorMessagesConstants.Media;
using static LKWSpringerApp.Common.EntityValidationConstants.Media;

namespace LKWSpringerApp.Data.Models
{
    public class Media
    {
        public Media()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Unique identifier.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = MediaImageUrlErrorMessage)]
        [Comment("ImageUrl of the client location and delivery area.")]
        public string ImageUrl { get; set; } = null!;

        [Comment("VideoUrl of the client location and delivery area.")]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? VideoUrl { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        [Comment("Description of the video or/and images.")]
        [MaxLength(DescriptionMaxLength)]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? Description { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        [Required]
        public Guid ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; } = null!;
    }
}
