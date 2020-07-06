using System.Collections.Generic;
using System.Linq;
using CinemaAPI;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CinemaDbContext _context;
        public UserRepository(CinemaDbContext context){
            _context = context;
        }
        public User Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void Delete(string login)
        {
            User userEntity = _context.Users.Where(a => a.Email == login).Single();
            _context.Users.Remove(userEntity);
            _context.SaveChanges();
        }

        public bool FindByEmail(string email)
        {
            if(_context.Users.ToList().Any(a => a.Email.Equals(email)))
                return true; 
            else
                return false;           
        }

        public User Get(int Id)
        {
            return _context.Users.Find(Id);
        }

        public List<User> GetList()
        {
            return _context.Users.ToList();
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.Where(a => a.Email == email).Single();
        }

        public User Update(User user)
        {
            _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return user;
        }
    }
}