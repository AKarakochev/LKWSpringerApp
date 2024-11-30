using static LKWSpringerApp.Common.EntityValidationConstants.ClientImage;

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LKWSpringerApp.Web.ViewModels.ClientImage
{
    public class EditClientImageModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Upload New Image")]
        public IFormFile? NewImageFile { get; set; }
        
        [Display(Name = "Video URL")]
        public string? VideoUrl { get; set; }
        [Display(Name = "Upload New Video")]
        public IFormFile? NewVideoFile { get; set; }

        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        [Display(Name = "Description")]
        public string? Description { get; set; }
    }
}
