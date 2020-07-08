using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class User
    {
        [Key]
        [Required]
        [DataMember]
        public int Id {get;set;}
        [Required]
        [DataMember]
        public string Name {get;set;}
        [Required]
        [DataMember]
        public string Surname {get;set;}
        [Required]
        [DataMember]
        public string Email {get;set;}
        [Required]
        public string Password {get;set;}
        [Required]
        [DataMember]
        public string UserType {get;set;}
        [Required]
        public ICollection<Reservation> Reservations {get; set;}
    }
}