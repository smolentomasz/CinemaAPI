namespace Models
{
    public class CreateMovieRequest
    {
        public string Name {get; set;}
        public string Description {get;set;}
        public string MoviePoster {get;set;}
        public int Duration {get;set;}

        public CreateMovieRequest(){

        }

        public Movie returnMovie(){
            var movie = new Movie{
                Name = this.Name,
                Description = this.Description,
                MoviePoster = this.MoviePoster,
                Duration = this.Duration
            };
            return movie;
        }
    }
}