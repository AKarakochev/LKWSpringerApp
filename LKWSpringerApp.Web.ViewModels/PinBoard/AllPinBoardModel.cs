using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKWSpringerApp.Web.ViewModels.PinBoard
{
    public class AllPinBoardModel
    {
        public string News { get; set; } = null!;
        public string ImportantNews { get; set; } = null!;
        public string? DrivingLicenseExpDate { get; set; }
        public string? DrivingCardExpDate { get; set; }
        public string? DrivingLicenseRenewalDate { get; set; }
        public string? DrivingCardRenewalDate { get; set; }
    }
}
