using System.Net.Http.Headers;
using System.IO;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;
using Newtonsoft.Json.Linq;

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
        [HttpPost("/api/movies/"), DisableRequestSizeLimit]
        public IActionResult AddNewMovie(){

            var detailsDecode = JObject.Parse(Request.Form["movieDetails"]);

            string filmName = detailsDecode["name"].ToString();
            string filmDescription = detailsDecode["description"].ToString();
            int filmDuration = Int32.Parse(detailsDecode["duration"].ToString());

            if(movieRepository.FindByName(filmName))
                return Conflict("Movie is existing in database!");
            else{
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources","Img");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);


                if(file.Length > 0){
                    var fullPath = Path.Combine(pathToSave, file.FileName);
                    var dbPath = Path.Combine(folderName,file.FileName);

                    using(var stream = new FileStream(fullPath, FileMode.Create)){
                        file.CopyTo(stream);
                    }

                    var filmDetails = new CreateMovieRequest{
                        Name = filmName,
                        Description = filmDescription,
                        Duration = filmDuration,
                        MoviePoster = dbPath
                    };
                    return Ok(movieRepository.Create(filmDetails.returnMovie()));
                }
                else{
                    return BadRequest();
                }
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
