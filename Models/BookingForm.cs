using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1.Models
{
    public class BookingForm
    {

        [Required]
        [Display(Name ="Start at",Description ="Booking Start Time")]
        public DateTimeOffset? StartAt { get; set; }

        [Required]
        [Display(Name = "End at", Description = "Booking End Time")]
        public DateTimeOffset? EndAt { get; set; }
    }
}
