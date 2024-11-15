using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKWSpringerApp.Web.ViewModels.ClientImage
{
    public class AllClientImageModel
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string? Description { get; set; }

    }
}
