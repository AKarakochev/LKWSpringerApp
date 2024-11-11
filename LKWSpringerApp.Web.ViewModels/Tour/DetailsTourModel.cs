using Microsoft.AspNetCore.Mvc.Rendering;

namespace LKWSpringerApp.Web.ViewModels.TourModels
{
    public class TourDetailsModel
    {
        public Guid Id { get; set; }
        public int TourNumber { get; set; }
        public string TourName { get; set; }

        // List of clients associated with the tour
        public List<ClientModelDetails> Clients { get; set; } = new List<ClientModelDetails>();

        // List of drivers associated with the tour
        public List<DriverModel> Drivers { get; set; } = new List<DriverModel>();
    }

    public class DriverModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }

    public class ClientModelDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
