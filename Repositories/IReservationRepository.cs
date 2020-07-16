using System.Collections.Generic;
using Models;

namespace Repositories
{
    public interface IReservationRepository
    {
        List<Reservation> GetList();
        Reservation Create(Reservation reservation);
        void Delete(string id);
        bool FindExistingReservation(string id);
        bool FindExistingReservation(int scheduleId, int seat);
        List<Reservation> GetListBySeance(int seanceId);
    }
}