using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class Movie
    {
        [Key]
        [Required]
        [DataMember]
        public int Id {get; set;}
        [Required]
        [DataMember]
        public string Name {get; set;}
        [Required]
        [DataMember]
        public string Description {get;set;}
        [Required]
        [DataMember]
        public string MoviePoster {get;set;}
        [Required]
        [DataMember]
        public int Duration {get;set;}
        [Required]
        public ICollection<Schedule> Schedules {get;set;}
    }
}