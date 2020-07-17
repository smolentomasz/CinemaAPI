using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;

namespace Controllers
{
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository scheduleRepository;
        private readonly IMovieRepository movieRepository;

        public ScheduleController(IScheduleRepository scheduleRepository, IMovieRepository movieRepository)
        {
            this.scheduleRepository = scheduleRepository;
            this.movieRepository = movieRepository;

        }

        [HttpGet("/api/schedule")]
        public IActionResult GetList()
        {
            return Ok(scheduleRepository.GetList());
        }
        [HttpGet("/api/schedule/single/{scheduleId}")]
        public IActionResult GetScheduleById(string scheduleId)
        {
            int scheduleIdInt = Int32.Parse(scheduleId);
            return Ok(scheduleRepository.GetScheduleById(scheduleIdInt));
        }
        [HttpGet("/api/schedule/{movieId}")]
        public IActionResult GetListByMovie(string movieId)
        {
            int movieIdInt = Int32.Parse(movieId);
            return Ok(scheduleRepository.GetListByMovie(movieIdInt));
        }
        [Authorize]
        [HttpPost("/api/schedule")]
        public IActionResult AddNewSchedule([FromBody] CreateScheduleRequest newSchedule)
        {
            if (newSchedule.Date.Equals("") || newSchedule.Time.Equals("") || newSchedule.MovieId.Equals(""))
            {
                return BadRequest("Missing or invalid data!");
            }
            else
            {
                if (scheduleRepository.FindExistingSchedule(newSchedule.Date, newSchedule.Time))
                {
                    return Conflict("Schedule with this date and time existing in database!");
                }
                else
                {
                    Movie movie = movieRepository.GetById(newSchedule.MovieId);
                    DateTime dateTime = DateTime.ParseExact(newSchedule.Time, "HH:mm:ss",
                                        CultureInfo.InvariantCulture);
                    DateTime max = DateTime.ParseExact("23:00:00", "HH:mm:ss",
                                        CultureInfo.InvariantCulture);
                    DateTime min = DateTime.ParseExact("08:00:00", "HH:mm:ss",
                                        CultureInfo.InvariantCulture);
                    if (dateTime.AddMinutes(movie.Duration) > max || dateTime < min)
                    {
                        return BadRequest("Bad time");
                    }
                    else
                    {
                        scheduleRepository.Create(newSchedule.returnSchedule());
                        return Ok(new ApiResponse("Schedule was added successfully!"));
                    }


                }
            }
        }
    }
}