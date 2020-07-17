using System.Collections.Generic;
using System.Linq;
using CinemaAPI;
using Models;

namespace Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaDbContext _context;
        public MovieRepository(CinemaDbContext context){
            _context = context;
        }
        public Movie Create(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return movie;
        }

        public bool FindByName(string name)
        {
            if(_context.Movies.ToList().Any(a => a.Name.Equals(name)))
                return true; 
            else
                return false;   
        }

        public Movie Get(string name)
        {
            return _context.Movies.Where(movie => movie.Name.Equals(name)).Single();
        }

        public Movie GetById(int movieId)
        {
            return _context.Movies.Where(movie => movie.Id.Equals(movieId)).Single();
        }

        public List<Movie> GetList()
        {
            return _context.Movies.ToList();
        }
    }
}