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

        public void Delete(string id)
        {
            Reservation reservationEntity = _context.Reservations.Where(a => a.Id == id).Single();
            _context.Reservations.Remove(reservationEntity);
            _context.SaveChanges();
        }

        public bool FindExistingReservation(string id)
        {
            if(_context.Reservations.ToList().Any(a => a.Id.Equals(id)))
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

        public List<Reservation> GetListByDate(string date)
        {
            return _context.Reservations.Include("Schedule").Include("Seat").Include("User").Include(b => b.Schedule.Movie).Where(a => a.Schedule.Date.Equals(date)).ToList();
        }
    }
}