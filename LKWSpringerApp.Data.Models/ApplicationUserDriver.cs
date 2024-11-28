using Microsoft.AspNetCore.Identity;

namespace LKWSpringerApp.Data.Models
{
    public class ApplicationUserDriver
    {
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        public Guid DriverId { get; set; }
        public virtual Driver Driver { get; set; }
    }
}
