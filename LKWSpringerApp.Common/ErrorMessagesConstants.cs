namespace LKWSpringerApp.Common
{
    public static class ErrorMessagesConstants
    {
        public static class Driver
        {
            public const string DriverFirstNameErrorMessage = "The first name of the driver is required.";
            public const string DriverSecondNameErrorMessage = "The second name of the driver is required.";
            public const string DriverBirthDateErrorMessage = "The birthdate of the driver is required.";
            public const string DriverBirthDateFormatErrorMessage = "Invalid Birth Date format.";
            public const string DriverMustBeEighteenYearsOldErrorMessage = "Driver must be at least 18 years old.";
            public const string DriverStartDateErrorMessage = "The date that the driver has started this job.";
            public const string DriverStartDateFormatErrorMessage = "Invalid Start Date format.";
            public const string DriverPhoneNumberErrorMessage = "The phone number of the driver is required.";

            public const string DriverInvalidIdErrorMessage = "Invalid driver ID.";
            public const string DriverOrTourInvalidIdErrorMessage = "Invalid driver or tour ID.";
            public const string DriverTryAgainErrorMessage = "An unexpected error occurred. Please try again later.";
        }

        public static class Client
        {
            public const string ClientNameErrorMessage = "The client name is required.";
            public const string ClientAddressErrorMessage = "The client address is required.";
            public const string ClientAddressUrlErrorMessage = "Please enter a valid Url";
            public const string ClientPhoneNumberErrorMessage = "The client phone number is required.";
            public const string ClientDeliveryDescriptionErrorMessage = "Delivery description is required.";
            public const string ClientDeliveryTimeErrorMessage = "Delivery time is required in format HH:MM - HH:MM";
            public const string ClientNumberErrorMessage = "The client number is required.";
            public const string ClientNumberRangeErrorMessage = "The number of the client must be between 1 and 10000.";
            
            
            public const string ClientLoadingErrorMessage = "An unexpected error occurred while loading clients.";
            public const string ClientInvalidIdErrorMessage = "Invalid client ID.";
            public const string ClientDatabaseErrorMessage = "Database error occurred. Please try again later.";
            public const string ClientTryAgainErrorMessage = "An unexpected error occurred.Please try again later.";
        }

        public static class Media
        {
            public const string MediaImageUrlErrorMessage = "ImageUrl is required.";

            public const string MediaInvalidIdErrorMessage = "Invalid client ID.";
            public const string MediaTryAgainErrorMessage = "An unexpected error occurred.Please try again later.";
            public const string MediaInvalidImageFormatErrorMessage = "Invalid image file format.";
            public const string MediaInvalidVideoFormatErrorMessage = "Invalid video file format.";
            public const string MediaIsDeletedOrNotFoundErrorMessage = "Media is deleted or not found.";
        }

        public static class Tour
        {
            public const string TourNumberErrorMessage = "Tour number is required.";
            public const string TourNameErrorMessage = "Tour name is required.";
            public const string TourRangeNumberErrorMessage = "Tour number must be between 1 and 1000.";
            public const string TourWithSameNumberErrorMessage = "A tour with the same name or number already exists.";

            public const string TourInvalidIdErrorMessage = "Invalid tour ID.";
            public const string TourTryAgainErrorMessage = "An unexpected error occurred.Please try again later.";
        }

        public static class PinBoard
        {
            public const string PinBoardDrivingLicenseExpDateErrorMessage = "Driving license ExpDate must be in format MM/YYYY and it is required field.";
            public const string PinBoardDrivingCardExpDateErrorMessage = "Driving card ExpDate must be in format MM/YYYY and it is required field.";
            public const string PinBoardDoNotHavePermission = "You do not have permission to view this information!";
            public const string PinBoardDetailsNotAvailable = "Details not available.";
            public const string PinBoardDriverInvalidId = "Invalid driver ID.";
            public const string PinBoardDriverDataNotFound = "Driver PinBoard data not found.";
            public const string PinBoardInvalidData = "Invalid data. Please correct the errors and try again.";
            
        }
    }
}
