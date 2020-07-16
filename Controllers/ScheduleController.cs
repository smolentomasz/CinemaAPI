using System;
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

        public ScheduleController(IScheduleRepository scheduleRepository)
        {
            this.scheduleRepository = scheduleRepository;

        }

        [HttpGet("/api/schedule")]
        public IActionResult GetList(){
            return Ok(scheduleRepository.GetList());
        }
        [HttpGet("/api/schedule/single/{scheduleId}")]
        public IActionResult GetScheduleById(string scheduleId){
            int scheduleIdInt = Int32.Parse(scheduleId);
            return Ok(scheduleRepository.GetScheduleById(scheduleIdInt));
        }
         [HttpGet("/api/schedule/{movieId}")]
        public IActionResult GetListByMovie(string movieId){
            int movieIdInt = Int32.Parse(movieId);
            return Ok(scheduleRepository.GetListByMovie(movieIdInt));
        }
        [Authorize]
        [HttpPost("/api/schedule")]
        public IActionResult AddNewSchedule([FromBody]CreateScheduleRequest newSchedule){
            if(newSchedule.Date.Equals("") || newSchedule.Time.Equals("") || newSchedule.MovieId.Equals("")){
                return BadRequest("Missing or invalid data!"); 
            }
            else{
                if(scheduleRepository.FindExistingSchedule(newSchedule.Date, newSchedule.Time)){
                    return Conflict("Schedule with this date and time existing in database!");
                }
                else{
                    return Ok(scheduleRepository.Create(newSchedule.returnSchedule()));
                }
            }
        }
    }
}