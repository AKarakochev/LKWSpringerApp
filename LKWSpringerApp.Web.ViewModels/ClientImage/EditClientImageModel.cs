using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LKWSpringerApp.Common.ErrorMessagesConstants.ClientImage;
using static LKWSpringerApp.Common.EntityValidationConstants.ClientImage;
using Microsoft.AspNetCore.Http;

namespace LKWSpringerApp.Web.ViewModels.ClientImage
{
    public class EditClientImageModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid ClientId { get; set; }

        [Required(ErrorMessage = ClientImageUrlErrorMessage)]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "New Image File")]
        public IFormFile? NewImageFile { get; set; } // Optional new file upload

        [Display(Name = "Video URL")]
        public string? VideoUrl { get; set; }

        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        [Display(Name = "Description")]
        public string? Description { get; set; }
    }
}
