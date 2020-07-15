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
        [HttpGet("/api/schedule/{id}")]
        public IActionResult GetListByMovie(string id){
            int movieId = Int32.Parse(id);
            return Ok(scheduleRepository.GetListByMovie(movieId));
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