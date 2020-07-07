using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Models
{
    [DataContract]
    public class Schedule
    {
        [Key]
        [Required]
        [DataMember]
        public int Id {get; set;}
        [Required]
        [DataMember]
        public string Date {get; set;}
        [Required]
        [DataMember]
        public string Time {get;set;}
        [Required]
        [DataMember]
        public int MovieId {get;set;}
        [Required]
        [DataMember]
        public Movie Movie {get;set;}
        [Required]
        public ICollection<Reservation> Reservations {get; set;}
    }
}