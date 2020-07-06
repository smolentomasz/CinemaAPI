using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ReservedSeat
    {
        [Key]
        [Required]
        public int Id {get; set;}

        public int SeatId {get; set;}
        public Seat Seat {get;set;}

        public string ReservationId {get; set;}
        public Reservation Reservation {get; set;}
    }
}