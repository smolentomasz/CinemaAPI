using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;

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
        [HttpPost("/api/reservations")]
        public IActionResult AddNewReservation([FromBody]CreateReservationRequest newReservation){
            List<int> reservedSeats = newReservation.SeatNumbers;
            
            foreach(int element in reservedSeats){
                var reservation = new Reservation{
                    Id = Guid.NewGuid().ToString("N"),
                    Paid = 25 * reservedSeats.Count,
                    ScheduleId = newReservation.ScheduleId,
                    SeatId = element
                };
                reservationRepository.Create(reservation);
            }
            return Ok("Added records to database!");
        }
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
        
    }
}