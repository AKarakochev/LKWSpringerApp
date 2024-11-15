using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LKWSpringerApp.Common.ErrorMessagesConstants.ClientImage;
using static LKWSpringerApp.Common.EntityValidationConstants.ClientImage;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace LKWSpringerApp.Web.ViewModels.ClientImage
{
    public class AddClientImageModel
    {
        [Required]
        [Display(Name = "Client")]
        public Guid ClientId { get; set; }

        [Required]
        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; } = null!;

        [Display(Name = "Video URL")]
        public string? VideoUrl { get; set; }

        [StringLength(DescriptionMaxLength,MinimumLength = DescriptionMinLength)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        public List<SelectListItem> Clients { get; set; } = new(); // For dropdown list
    }
}
