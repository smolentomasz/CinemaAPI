using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models
{
    [DataContract]
    public class Seat
    {
        [Key]
        [Required]
        [DataMember]
        public int Id {get;set;}
        [Required]
        [DataMember]
        public int SeatNumber {get;set;}
        [Required]
        [DataMember]
        public int SeatRow {get;set;}
        public ICollection<Reservation> Reservations {get; set;}
    }
}