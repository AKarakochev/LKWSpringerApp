using LKWSpringerApp.Web.ViewModels.Tour;

namespace LKWSpringerApp.Web.ViewModels.Driver
{
    public class DetailsDriverModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public string BirthDate { get; set; } = null!;
        public string StartDate { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public bool Springerdriver { get; set; }
        public bool Stammdriver { get; set; }
        public List<TourViewModel> Tours { get; set; } = new List<TourViewModel>();
    }
}
