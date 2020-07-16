using System.Net;
using System.Text;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Controllers
{
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository reservationRepository;

        public ReservationController(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }
        [HttpGet("/api/reservations")]
        public IActionResult GetList(){
            return Ok(reservationRepository.GetList());
        }
        [Authorize]
        [HttpPost("/api/reservations")]
        public IActionResult AddNewReservation([FromBody]CreateReservationRequest newReservation){
            List<int> reservedSeats = newReservation.SeatNumbers;
            
            foreach(int element in reservedSeats){
                if(reservationRepository.FindExistingReservation(newReservation.ScheduleId, element)){
                    return Conflict("Seat with number " + element + " is not available!");
                }
                else{
                    var reservation = new Reservation{
                        Paid = 25 * reservedSeats.Count,
                        ScheduleId = newReservation.ScheduleId,
                        SeatId = element,
                        UserId = newReservation.UserId
                };
                reservationRepository.Create(reservation);
                }
                
            }
            ApiResponse response = new ApiResponse("Added records to database!");
            return Ok(response);
        }
        [Authorize]
        [HttpDelete("/api/reservations/{id}")]
        public IActionResult DeleteReservation(string id){
            if(reservationRepository.FindExistingReservation(id)){
                reservationRepository.Delete(id);
                return Ok("Reservation deleted succesfully!");
            }
            else{
                return BadRequest("Reservation with this id doesn't exist in database!");
            }
            
        }
        [Authorize]
        [HttpGet("/api/reservations/{seanceId}")]
        public IActionResult GetReservationBySeance(string seanceId){
            int seanceIdInt = Int32.Parse(seanceId);
            
            return Ok(reservationRepository.GetListBySeance(seanceIdInt));
        }
        
    }
}