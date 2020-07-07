using System.Collections.Generic;
using System.Linq;
using CinemaAPI;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly CinemaDbContext _context;
        public ScheduleRepository(CinemaDbContext context){
            _context = context;
        }
        public Schedule Create(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            _context.SaveChanges();
            return schedule;
        }

        public bool FindExistingSchedule(string date, string time)
        {
            if(_context.Schedules.ToList().Any(a => a.Date.Equals(date) && a.Time.Equals(time)))
                return true; 
            else
                return false;   
        }

        public List<Schedule> GetList()
        {
            return _context.Schedules.Include("Movie").ToList();
        }
    }
}