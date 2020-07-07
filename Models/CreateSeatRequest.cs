namespace Models
{
    public class CreateSeatRequest
    {
        public int SeatNumber {get;set;}
        public int SeatRow {get;set;}

        public CreateSeatRequest(){

        }

        public Seat returnSeat(){
            var seat = new Seat{
                SeatNumber = this.SeatNumber,
                SeatRow = this.SeatRow
            };
            return seat;
        }

    }
}