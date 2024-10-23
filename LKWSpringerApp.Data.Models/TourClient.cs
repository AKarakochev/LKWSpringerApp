using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LKWSpringerApp.Data.Models
{
    [PrimaryKey(nameof(TourId),nameof(ClientId))]
    public class TourClient
    {
        [Required]
        [Comment("Unique identifier.")]
        public Guid TourId { get; set; }

        [ForeignKey(nameof(TourId))]
        public Tour Tour { get; set; } = null!;

        [Required]
        public Guid ClientId { get; set; }
        
        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; } = null!;
    }
}
