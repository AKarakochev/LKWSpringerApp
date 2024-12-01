using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using static LKWSpringerApp.Common.EntityValidationConstants.Media;

namespace LKWSpringerApp.Web.ViewModels.Media
{
    public class AddMediaModel
    {
        [Required]
        [Display(Name = "Client")]
        public Guid ClientId { get; set; }

        [Required]
        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; } = null!;

        [Display(Name = "Video URL")]
        public IFormFile? VideoFile { get; set; }

        [StringLength(DescriptionMaxLength,MinimumLength = DescriptionMinLength)]
        [Display(Name = "Description")]
        public string? Description { get; set; }
        public List<SelectListItem> Clients { get; set; } = new();
    }
}
