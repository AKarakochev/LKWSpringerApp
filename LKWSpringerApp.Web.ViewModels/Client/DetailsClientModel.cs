namespace LKWSpringerApp.Web.ViewModels.Client
{
    public class DetailsClientModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int ClientNumber { get; set; }
        public string Address { get; set; } = null!;
        public string? AddressUrl { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string DeliveryDescription { get; set; } = null!;
        public string DeliveryTime { get; set; } = null!;
        public List<ClientImageModel> Images { get; set; } = new List<ClientImageModel>();
    }
}
