namespace LKWSpringerApp.Web.ViewModels.Client
{
    public class AllClientModel
    { 
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int ClientNumber { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string DeliveryDescription { get; set; } = null!;
        public string DeliveryTime { get; set; } = null!;
        public string? AddressUrl { get; set; }
        public List<ClientMediaModel> Images { get; set; } = new List<ClientMediaModel>();
    }

    public class ClientMediaModel
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string? VideoUrl { get; set; }
        public string? Description { get; set; }
    }
}
