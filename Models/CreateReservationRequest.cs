using System.Collections.Generic;

namespace Models
{
    public class CreateReservationRequest
    {
        public int Paid {get; set;}
        public int ScheduleId {get;set;}
        
        public List<int> SeatNumbers {get;set;}

        public CreateReservationRequest(){

        }
    }
}