using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LKWSpringerApp.Web.ViewModels.Client
{
    public class ClientViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int ClientNumber { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string DeliveryDescription { get; set; } = null!;
        public string DeliveryTime { get; set; } = null!;
        public string? AddressUrl { get; set; }

        // Collection of images related to the client
        public List<ClientImageModel> Images { get; set; } = new List<ClientImageModel>();
    }

    public class ClientImageModel
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string? VideoUrl { get; set; }
        public string? Description { get; set; }
    }
}
