using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Schedule
    {
        [Key]
        [Required]
        public int Id {get; set;}
        [Required]
        public string Date {get; set;}
        [Required]
        public string Time {get;set;}
        [Required]
        public int MovieId {get;set;}
        public Movie Movie {get;set;}
        [Required]
        public List<Reservation> Reservations {get; set;}
    }
}