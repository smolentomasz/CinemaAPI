using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Movie
    {
        [Key]
        [Required]
        public int Id {get; set;}
        [Required]
        public string Name {get; set;}
        [Required]
        public string Description {get;set;}
        [Required]
        public byte[] MoviePoster {get;set;}
        [Required]
        public int Duration {get;set;}
        [Required]
        public virtual List<Schedule> Schedules {get;set;}
    }
}