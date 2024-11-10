using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<string> Tours { get; set; } = new List<string>();
    }
}
