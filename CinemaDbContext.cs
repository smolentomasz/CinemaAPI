using Microsoft.EntityFrameworkCore;
using Models;

namespace CinemaAPI
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options){

        }

        public DbSet<Movie> Movies {get;set;}
        public DbSet<User> Users {get; set;}

        public DbSet<Reservation> Reservations {get; set;}
        public DbSet<Schedule> Schedules {get;set;}
        public DbSet<Seat> Seats {get;set;}
    }
}