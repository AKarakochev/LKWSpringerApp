using System.ComponentModel.DataAnnotations;

namespace LKWSpringerApp.Web.ViewModels.Media
{
    public class DeleteMediaModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Description")]
        public string? Description { get; set; }
    }
}
