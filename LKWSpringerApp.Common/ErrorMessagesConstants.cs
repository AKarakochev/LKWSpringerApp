namespace LKWSpringerApp.Common
{
    public static class ErrorMessagesConstants
    {
        public static class Driver
        {
            public const string DriverFirstNameErrorMessage = "The first name of the driver is required.";
            public const string DriverSecondNameErrorMessage = "The second name of the driver is required.";
            public const string DriverBirthDateErrorMessage = "The birthdate of the driver is required.";
            public const string DriverStartDateErrorMessage = "The date that the driver has started this job.";
            public const string DriverPhoneNumberErrorMessage = "The phone number of the driver is required.";
        }

        public static class Client
        {
            public const string ClientNameErrorMessage = "The client name is required.";
            public const string ClientAddressErrorMessage = "The client address is required.";
            public const string ClientPhoneNumberErrorMessage = "The client phone number is required.";
            public const string ClientDeliveryDescriptionErrorMessage = "Delivery description is required.";
            public const string ClientDeliveryTimeErrorMessage = "Delivery time is required in format HH:MM - HH:MM";
            public const string ClientNumberErrorMessage = "The client number is required.";
            public const string ClientNumberRangeErrorMessage = "The number of the client must be between 1 and 10000.";
        }

        public static class ClientImage
        {
            public const string ClientImageUrlErrorMessage = "ImageUrl is required.";
        }

        public static class Tour
        {
            public const string TourNumberErrorMessage = "Tour number is required.";
            public const string TourNameErrorMessage = "Tour name is required.";
            public const string TourRangeNumberErrorMessage = "Tour number must be between 1 and 1000.";
        }
    }
}
