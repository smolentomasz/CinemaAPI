using System.Collections.Generic;
using Models;

namespace Repositories
{
    public interface IScheduleRepository
    {
        List<Schedule> GetList();
        Schedule Create(Schedule schedule);
        bool FindExistingSchedule(string date, string time);

        List<Schedule> GetListByMovie(int movieId);
    }
}