using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;

namespace CinemaAPI.Controllers
{
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;

        }
        [HttpGet("/api/movies")]
        public IActionResult GetList(){
            return Ok(movieRepository.GetList());
        }
        [Authorize]
        [HttpPost("/api/movies/")]
        public IActionResult AddNewMovie([FromBody]CreateMovieRequest createMovie){
            if(createMovie.Name.Equals("") || createMovie.Description.Equals("") || createMovie.MoviePoster.Equals("") || createMovie.Duration.Equals("")){
                return BadRequest("Missing or invalid data!"); 
            }
            else{
                if(movieRepository.FindByName(createMovie.Name))
                    return Conflict("Movie is existing in database!");
                else
                    return Ok(movieRepository.Create(createMovie.returnMovie()));
            }
        }
        [HttpGet("/api/movies/name")]
        public IActionResult GetMovieByName(string name){
            if(movieRepository.FindByName(name)){
                return Ok(movieRepository.Get(name));
            }
            else{
                return BadRequest("Movie with this title doesn't exist in database!");
            }
        }
        
    }
}