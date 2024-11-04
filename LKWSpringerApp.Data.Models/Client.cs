using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static LKWSpringerApp.Common.EntityValidationConstants.Client;
using static LKWSpringerApp.Common.ErrorMessagesConstants.Client;


namespace LKWSpringerApp.Data.Models
{
    public class Client
    {
        public Client()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Unique identifier.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = ClientNameErrorMessage)]
        [Comment("The name of the client.")]
        [MaxLength(ClientNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = ClientNumberErrorMessage)]
        [Comment("The number of the client is required.")]
        [Range(1,10000,ErrorMessage = ClientNumberRangeErrorMessage)]
        public int ClientNumber { get; set; }

        [Required(ErrorMessage = ClientAddressErrorMessage)]
        [Comment("The address of the client.")]
        [MaxLength(ClientAddressMaxLength)]
        public string Address { get; set; } = null!;

        [MaxLength(ClientAddressUrlMaxLength)]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? AddressUrl { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        [Required(ErrorMessage = ClientPhoneNumberErrorMessage)]
        [Comment("The phone number of the client.")]
        [RegularExpression(ClientPhoneNumberFormatPattern)]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = ClientDeliveryDescriptionErrorMessage)]
        [Comment("How the client want we to make his delivery.")]
        [MaxLength(DeliveryDescriptionMaxLength)]
        public string DeliveryDescription { get; set; } = null!;

        [Required(ErrorMessage = ClientDeliveryTimeErrorMessage)]
        [Comment("When the delivery must be made.")]
        [RegularExpression(ClientDeliveryTimeRegexFormat)]
        public string DeliveryTime { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public ICollection<TourClient> ClientsTours { get; set; } = new HashSet<TourClient>();

        public ICollection<ClientImage> Images { get; set; } = new List<ClientImage>();
    }
}
