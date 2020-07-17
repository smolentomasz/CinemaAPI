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

        public bool FindExistingScheduleByDate(string date)
        {
             if(_context.Schedules.ToList().Any(a => a.Date.Equals(date)))
                return true; 
            else
                return false;  
        }

        public List<Schedule> GetList()
        {
            return _context.Schedules.Include("Movie").ToList();
        }

        public List<Schedule> GetListByDate(string date)
        {
            return _context.Schedules.Include("Movie").Where(a => a.Date.Equals(date)).ToList();
        }

        public List<Schedule> GetListByMovie(int movieId)
        {
            return _context.Schedules.Where(a => a.MovieId == movieId).ToList();
        }

        public Schedule GetScheduleById(int scheduleId)
        {
           return _context.Schedules.Include("Movie").Where(a => a.Id == scheduleId).Single();
        }
    }
}