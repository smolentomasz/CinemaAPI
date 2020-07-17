using System.Security.AccessControl;
using System.Collections.Generic;

namespace Models
{
    public class ReservationInfo
    {
        public List<Seat> seats {get;set;}
        public string uuid {get;set;}
        public User user {get;set;}
        public Schedule schedule {get;set;}
        public int paid {get;set;}

        public ReservationInfo(List<Seat> seats, string uuid, User user, Schedule schedule, int paid){
            this.seats = seats;
            this.uuid = uuid;
            this.user = user;
            this.schedule = schedule;
            this.paid = paid;
        }
    }
}