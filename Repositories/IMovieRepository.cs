using System.Collections.Generic;
using Models;

namespace Repositories
{
    public interface IMovieRepository
    {
        Movie Get(string name);
        List<Movie> GetList();
        Movie Create(Movie movie);
        bool FindByName(string name);
    }
}