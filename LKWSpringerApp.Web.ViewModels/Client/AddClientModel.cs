using static LKWSpringerApp.Common.EntityValidationConstants.Client;
using static LKWSpringerApp.Common.ErrorMessagesConstants.Client;

using System.ComponentModel.DataAnnotations;

namespace LKWSpringerApp.Web.ViewModels.Client
{
    public class AddClientModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = ClientNameErrorMessage)]
        [StringLength(ClientNameMaxLength, MinimumLength = ClientNameMinLength)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = ClientNumberErrorMessage)]
        [Range(1, 10000, ErrorMessage = ClientNumberRangeErrorMessage)]
        public int? ClientNumber { get; set; }

        [Required(ErrorMessage = ClientAddressErrorMessage)]
        [StringLength(ClientAddressMaxLength, MinimumLength = ClientAddressMinLength)]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = ClientPhoneNumberErrorMessage)]
        [RegularExpression(ClientPhoneNumberFormatPattern)]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = ClientDeliveryDescriptionErrorMessage)]
        [StringLength(DeliveryDescriptionMaxLength, MinimumLength = DeliveryDescriptionMinLength)]
        public string DeliveryDescription { get; set; } = null!;

        [Required(ErrorMessage = ClientDeliveryTimeErrorMessage)]
        [RegularExpression(ClientDeliveryTimeRegexFormat)]
        public string DeliveryTime { get; set; } = null!;

        [StringLength(ClientAddressUrlMaxLength)]
        public string? AddressUrl { get; set; }
    }
}
