namespace Models
{
    public class CreateScheduleRequest
    {
        public string Date {get; set;}
        public string Time {get;set;}
        public int MovieId {get;set;}

        public CreateScheduleRequest(){

        }

        public Schedule returnSchedule(){
            var schedule = new Schedule{
                Date = this.Date,
                Time = this.Time,
                MovieId = this.MovieId
            };

            return schedule;
        }
    }
}