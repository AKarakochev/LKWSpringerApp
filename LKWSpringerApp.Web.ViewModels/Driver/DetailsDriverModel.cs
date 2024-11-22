namespace LKWSpringerApp.Web.ViewModels.Driver
{
    public class DetailsDriverModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string BirthDate { get; set; }
        public string StartDate { get; set; }
        public string PhoneNumber { get; set; }
        public bool Springerdriver { get; set; }
        public bool Stammdriver { get; set; }
        public List<TourViewModel> Tours { get; set; } = new List<TourViewModel>();
    }
}
