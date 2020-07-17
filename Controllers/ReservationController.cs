using System.Net.Mail;
using System.Net;
using System.Text;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using MimeKit.Text;

namespace Controllers
{
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IScheduleRepository scheduleRepository;
        private readonly IUserRepository userRepository;
        private readonly ISeatRepository seatRepository;

        public ReservationController(IReservationRepository reservationRepository, IScheduleRepository scheduleRepository, IUserRepository userRepository, ISeatRepository seatRepository)
        {
            this.reservationRepository = reservationRepository;
            this.scheduleRepository = scheduleRepository;
            this.userRepository = userRepository;
            this.seatRepository = seatRepository;
        }
        [HttpGet("/api/reservations")]
        public IActionResult GetList(){
            return Ok(reservationRepository.GetList());
        }
        [Authorize]
        [HttpPost("/api/reservations")]
        public IActionResult AddNewReservation([FromBody]CreateReservationRequest newReservation){
            List<int> reservedSeats = newReservation.SeatNumbers;
            List<Seat> reservedSeatsModel = new List<Seat>();
            DateTime localDate = DateTime.Now;
            StringBuilder uniqueUUID = new StringBuilder();

            uniqueUUID.Append(localDate.ToString() + "ID:" +  newReservation.UserId + "SCH:" + newReservation.ScheduleId);

            Schedule infoSchedule = scheduleRepository.GetScheduleById(newReservation.ScheduleId);
            User infoUser = userRepository.GetUserById(newReservation.UserId);

            ReservationInfo reservationInfo = new ReservationInfo(reservedSeatsModel, uniqueUUID.ToString(), infoUser, infoSchedule, newReservation.Paid);
            
            foreach(int element in reservedSeats){
                if(reservationRepository.FindExistingReservation(newReservation.ScheduleId, element)){
                    return Conflict("Seat with number " + element + " is not available!");
                }
                else{
                    reservedSeatsModel.Add(seatRepository.Get(element));
                    var reservation = new Reservation{
                        ReservationUUID = uniqueUUID.ToString(),
                        Paid = newReservation.Paid,
                        ScheduleId = newReservation.ScheduleId,
                        SeatId = element,
                        UserId = newReservation.UserId,
                      
                };
                reservationRepository.Create(reservation);
                }
                
            }
            ApiResponse response = new ApiResponse("Added records to database!");
            MailModel.sendMessage(reservationInfo, "ADD");
            return Ok(response);
        }
        [Authorize]
        [HttpDelete("/api/reservations/{uuid}")]
        public IActionResult DeleteReservation(string uuid){
            if(reservationRepository.FindExistingReservation(uuid)){
                reservationRepository.Delete(uuid);
                 MailModel.sendMessage(null, "DELETE");
                return Ok(new ApiResponse("Reservation deleted succesfully!"));
            }
            else{
                return BadRequest(new ApiResponse("Reservation with this id doesn't exist in database!"));
            }
            
        }
        [Authorize]
        [HttpGet("/api/reservations/{seanceId}")]
        public IActionResult GetReservationBySeance(string seanceId){
            int seanceIdInt = Int32.Parse(seanceId);
            
            return Ok(reservationRepository.GetListBySeance(seanceIdInt));
        }
        [Authorize]
        [HttpGet("/api/reservations/user/{userId}")]
        public IActionResult GetReservationByUser(int userId){            
            return Ok(reservationRepository.GetListByUserId(userId));
        }
        
    }
}