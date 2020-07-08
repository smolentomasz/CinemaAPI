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
        [Authorize]
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
            return Ok("Added records to database!");
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
        [HttpGet("/api/reservations/{date}")]
        public IActionResult GetReservationsByDate(string date){
            DateTime oDate = DateTime.Parse(date);
            StringBuilder dateBuilder  = new StringBuilder();

            if(oDate.Day < 10)
                dateBuilder.Append("0" + oDate.Day  + "/");
            else
                dateBuilder.Append(oDate.Day  + "/");
            if(oDate.Month < 10)
                dateBuilder.Append("0" + oDate.Month  + "/");
            else
                dateBuilder.Append(oDate.Month  + "/");

            dateBuilder.Append(oDate.Year);

            //return Ok(dateBuilder.ToString());
            return Ok(reservationRepository.GetListByDate(dateBuilder.ToString()));
        }
        
    }
}