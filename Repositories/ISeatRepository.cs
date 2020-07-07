using System.Collections.Generic;
using Models;

namespace Repositories
{
    public interface ISeatRepository
    {
        Seat Create(Seat seat);
        Seat Get(int id);
        List<Seat> GetList();
        bool FindExistingSeat(int id);
        bool FindExistingSeat(int seatNumber, int seatRow);
    }
}