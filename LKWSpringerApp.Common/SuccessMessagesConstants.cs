using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKWSpringerApp.Common
{
    public static class SuccessMessagesConstants
    {
        public static class Client
        {
            public const string ClientAddedSuccessMessage = "Client added successfully.";
            public const string ClientUpdatedSuccessMessage = "Client updated successfully.";
            public const string ClientDeletedSuccessMessage = "Client deleted successfully.";
        }

        public static class ClientImage
        {
            public const string ClientImageDeletedSuccessMessage = "Client image deleted successfully.";
            public const string ClientImageAddedSuccessMessage = "Client image added successfully.";
            public const string ClientImageUpdatedSuccessMessage = "Client image updated successfully.";
        }

        public static class Driver
        {
            public const string DriverAddedSuccessMessage = "Driver added successfully.";
            public const string DriverUpdatedSuccessMessage = "Driver updated successfully.";
            public const string DriverDeletedSuccessMessage = "Driver deleted successfully.";
            public const string DriverTourDeletedSuccessMessage = "Tour removed from driver successfully.";
        }

        public static class Tour
        {
            public const string TourDeletedSuccessMessage = "Tour deleted successfully.";
            public const string TourAddedSuccessMessage = "Tour added successfully.";
            public const string TourUpdatedSuccessMessage = "Tour updated successfully.";
        }
    }
}
