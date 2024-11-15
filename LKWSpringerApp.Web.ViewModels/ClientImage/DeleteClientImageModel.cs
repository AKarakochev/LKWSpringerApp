using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKWSpringerApp.Web.ViewModels.ClientImage
{
    public class DeleteClientImageModel
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
