using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKWSpringerApp.Data.Models
{
    public class ApplicationUserDriver
    {
        public string UserId { get; set; }  // FK to IdentityUser
        public virtual IdentityUser User { get; set; }

        public Guid DriverId { get; set; }  // FK to Driver
        public virtual Driver Driver { get; set; }
    }
}
