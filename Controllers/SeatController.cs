using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;

namespace Controllers
{
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly ISeatRepository seatRepository;

        public SeatController(ISeatRepository seatRepository)
        {
            this.seatRepository = seatRepository;

        }

        [HttpGet("/api/seat")]
        public IActionResult GetList(){
            return Ok(seatRepository.GetList());
        }
        [HttpGet("/api/seat/{id}")]
        public IActionResult GetSeatById(int id){
            if(seatRepository.FindExistingSeat(id))
                return Ok(seatRepository.Get(id));
            else
                return Conflict("Seat with this number doesn't exist in database!");
        }
        
        [Authorize]
        [HttpPost("/api/seat")]
        public IActionResult AddNewSeat([FromBody]CreateSeatRequest newSeat){
            if(newSeat.SeatNumber.Equals("") || newSeat.SeatRow.Equals("")){
                return BadRequest("Missing or invalid data!"); 
            }
            else{
                if(seatRepository.FindExistingSeat(newSeat.SeatNumber, newSeat.SeatRow)){
                    return Conflict("Seat with this data is existing in database!");
                }
                else{
                    return Ok(seatRepository.Create(newSeat.returnSeat()));
                }
            }
        }
        
    }
}