using Microsoft.AspNetCore.Identity;

namespace LKWSpringerApp.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            this.Id = Guid.NewGuid();
        }

        public virtual ICollection<ApplicationUserDriver> ApplicationUserDrivers { get; set; }
            = new HashSet<ApplicationUserDriver>();
    }

}
