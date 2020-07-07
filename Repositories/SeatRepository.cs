using System.Collections.Generic;
using System.Linq;
using CinemaAPI;
using Models;

namespace Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly CinemaDbContext _context;
        public SeatRepository(CinemaDbContext context){
            _context = context;
        }
        public Seat Create(Seat seat)
        {
            _context.Seats.Add(seat);
            _context.SaveChanges();
            return seat;
        }

        public bool FindExistingSeat(int id)
        {
            if(_context.Seats.ToList().Any(a => a.Id.Equals(id)))
                return true; 
            else
                return false; 
        }

        public bool FindExistingSeat(int seatNumber, int seatRow)
        {
            if(_context.Seats.ToList().Any(a => a.SeatNumber.Equals(seatNumber) && a.SeatRow.Equals(seatRow)))
                return true; 
            else
                return false; 
        }

        public Seat Get(int id)
        {
            return _context.Seats.Where(a => a.Id.Equals(id)).Single();
        }

        public List<Seat> GetList()
        {
            return _context.Seats.ToList();
        }
    }
}