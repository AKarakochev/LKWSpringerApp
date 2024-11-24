namespace LKWSpringerApp.Web.ViewModels.TourModels
{
    public class DetailsTourModel
    {
        public Guid Id { get; set; }
        public int TourNumber { get; set; }
        public string TourName { get; set; } = null!;
        public List<ClientModelDetails> Clients { get; set; } = new List<ClientModelDetails>();
        public List<DriverModel> Drivers { get; set; } = new List<DriverModel>();
    }

    public class DriverModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
    }

    public class ClientModelDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
