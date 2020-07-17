using System.Collections.Generic;
using System.Linq;
using CinemaAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;

namespace Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly CinemaDbContext _context;
         
        public ReservationRepository(CinemaDbContext context){
            _context = context;
           
        }
        public Reservation Create(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return reservation;
        }

        public void Delete(string uuid)
        {
            List<Reservation> reservationEntity = _context.Reservations.Where(a => a.ReservationUUID.Equals(uuid)).ToList();
            foreach (Reservation element in reservationEntity){
                _context.Reservations.Remove(element);
                _context.SaveChanges();
            }    
        }

        public bool FindExistingReservation(string uuid)
        {
            if(_context.Reservations.ToList().Any(a => a.ReservationUUID.Equals(uuid)))
                return true; 
            else
                return false;   
        }

        public bool FindExistingReservation(int schedule, int seat)
        {
            if(_context.Reservations.ToList().Any(a => a.ScheduleId.Equals(schedule) && a.SeatId.Equals(seat)))
                return true; 
            else
                return false;
        }

        public List<Reservation> GetList()
        {
            return _context.Reservations.Include("Schedule").Include("Seat").Include("User").Include(b => b.Schedule.Movie).ToList();
        }

        public List<Reservation> GetListBySeance(int seanceId)
        {
            return _context.Reservations.Include("Schedule").Include("Seat").Include("User").Include(b => b.Schedule.Movie).Where(a => a.Schedule.Id.Equals(seanceId)).ToList();
        }

        public List<Reservation> GetListByUserId(int userId)
        {
            return _context.Reservations.Include("Schedule").Include("Seat").Include("User").Include(b => b.Schedule.Movie).Where(a => a.UserId.Equals(userId)).ToList();
        }
    }
}