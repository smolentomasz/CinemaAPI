using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Reservation
    {
        [Key]
        [Required]
        public string Id {get;set;}
        [Required]
        public int Paid {get; set;}
        [Required]
        public int ScheduleId {get;set;}
        public Schedule Schedule {get;set;}

        public virtual List<ReservedSeat> Seats {get;set;}
    }
}