using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Seat
    {
        [Key]
        [Required]
        public int Id {get;set;}
        [Required]
        public int SeatNumber {get;set;}
        [Required]
        public int SeatRow {get;set;}

        public virtual List<ReservedSeat> Seats {get;set;}
    }
}