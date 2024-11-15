using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKWSpringerApp.Common
{
    public static class EntityValidationConstants
    {
        public static class Driver
        {
            public const int DriverFirstNameMaxLength = 50;
            public const int DriverFirstNameMinLength = 3;
            public const int DriverSecondNameMaxLength = 50;
            public const int DriverSecondNameMinLength = 3;
            public const string DriverBirthDateFormat = "dd/MM/yyyy";
            public const string DriverStartDateFormat = "dd/MM/yyyy";
            public const string DriverPhoneNumberFormatPattern = @"^(\+?\d{10,14})$";
        }

        public static class Client
        {
            public const int ClientNameMaxLength = 100;
            public const int ClientNameMinLength = 3;
            public const int ClientAddressMaxLength = 250;
            public const int ClientAddressMinLength = 10;
            public const int ClientAddressUrlMaxLength = 150;
            public const string ClientPhoneNumberFormatPattern = @"^(\+?\d{10,14})$";
            public const int DeliveryDescriptionMaxLength = 250;
            public const int DeliveryDescriptionMinLength = 10;
            public const string ClientDeliveryTimeRegexFormat = @"^\d{2}:\d{2}$";
        }

        public static class Tour
        {
            public const int TourNameMaxLength = 100;
            public const int TourNameMinLength = 4;
        }

        public static class ClientImage
        {
            public const int DescriptionMaxLength = 500;
            public const int DescriptionMinLength = 5;
        }
    }
}
