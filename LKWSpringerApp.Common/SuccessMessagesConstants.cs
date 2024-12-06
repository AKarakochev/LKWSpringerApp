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

        public static class Media
        {
            public const string MediaDeletedSuccessMessage = "Client image deleted successfully.";
            public const string MediaAddedSuccessMessage = "Client image added successfully.";
            public const string MediaUpdatedSuccessMessage = "Client image updated successfully.";
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

        public static class PinBoard
        {
            public const string PinBoardNewsUpdated = "News updated successfully.";
            public const string PinBoardDetailsUpdate = "Details updated successfully.";
        }
    }
}
