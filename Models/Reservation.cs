using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Reservation
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id {get;set;}
        [Required]
        public int Paid {get; set;}
        [Required]
        public int ScheduleId {get;set;}
        public Schedule Schedule {get;set;}
        public int UserId {get;set;}
        public User User {get;set;}
        public int SeatId {get; set;}
        public Seat Seat {get;set;}
    }
}