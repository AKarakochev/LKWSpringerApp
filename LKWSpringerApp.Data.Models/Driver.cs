﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static LKWSpringerApp.Common.EntityValidationConstants.Driver;
using static LKWSpringerApp.Common.ErrorMessagesConstants.Driver;

namespace LKWSpringerApp.Data.Models
{
    public class Driver
    {
        public Driver()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Comment("Unique identifier.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = DriverFirstNameErrorMessage)]
        [Comment("Driver first name.")]
        [MaxLength(DriverFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = DriverSecondNameErrorMessage)]
        [Comment("Driver second name.")]
        [MaxLength(DriverSecondNameMaxLength)]
        public string SecondName { get; set; } = null!;
        
        [Required(ErrorMessage = DriverBirthDateErrorMessage)]
        [Comment("Driver birthdate.")]
        [DisplayFormat(DataFormatString = DriverBirthDateFormat, ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = DriverStartDateErrorMessage)]
        [Comment("Date of commencement of employment.")]
        [DisplayFormat(DataFormatString = DriverStartDateFormat, ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = DriverPhoneNumberErrorMessage)]
        [Comment("The phone number of the driver.")]
        [RegularExpression(DriverPhoneNumberFormatPattern)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [Comment("That is a driver who visits different clients almost every day.")]
        public bool Springerfahrer { get; set; }

        [Required]
        [Comment("That is a driver who visits the same clients.")]
        public bool Stammfahrer { get; set; }

        [Required]
        [Comment("Shows if a driver has been deleted.")]
        public bool IsDeleted { get; set; }

        //[Required]
        //public string UserId { get; set; }

        //[ForeignKey(nameof(UserId))]
        //public IdentityUser User { get; set; } = null!;

        public ICollection<Tour> Tours { get; set; } = new HashSet<Tour>();
    }
}
