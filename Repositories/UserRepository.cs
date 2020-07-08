using System;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using CinemaAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;
using Microsoft.IdentityModel.Tokens;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CinemaDbContext _context;
        private readonly JwtSettings _jwtSettings;
        public UserRepository(CinemaDbContext context, IOptions<JwtSettings> jwtsettings){
            _context = context;
             _jwtSettings = jwtsettings.Value;
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

        public UserToken GetUserTokenByEmail(string email, string password)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            UserToken userToken = new UserToken(_context.Users.Where(a => a.Email == email).Single());

            userToken.Token = tokenHandler.WriteToken(token);
            

            return userToken;
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