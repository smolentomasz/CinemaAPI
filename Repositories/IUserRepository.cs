using System.Collections.Generic;
using Models;

namespace Repositories
{
    public interface IUserRepository
    {
        User Get(int Id);
        List<User> GetList();
        User Create(User user);
        User Update(User user);
        void Delete(string login);
        bool FindByEmail(string email);
        User GetUserByEmail(string email);
    }
}