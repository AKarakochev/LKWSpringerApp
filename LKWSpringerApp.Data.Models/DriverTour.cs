namespace LKWSpringerApp.Data.Models
{
    public class DriverTour
    {
        public Guid DriverId { get; set; }
        public Driver Driver { get; set; } = null!;

        public Guid TourId { get; set; }
        public Tour Tour { get; set; } = null!;
    }
}
