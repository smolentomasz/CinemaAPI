using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System;

namespace CinemaAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;

        }
        //GET all users
        [Authorize]
        [HttpGet("/api/users")]
        public IActionResult GetList(){
            return Ok(userRepository.GetList());
        }
        //ADD User
        [HttpPost("/api/users/register")]
        public IActionResult Post([FromBody]CreateUserRequest createUser)
        {
        if(createUser.Name.Equals("") || createUser.Surname.Equals("") || createUser.Email.Equals("") || createUser.Password.Equals("")){
            return BadRequest("Missing or invalid data!"); 
        }
        else{
            if(userRepository.FindByEmail(createUser.Email))
                return Conflict("User with this email is existing in database!");
            else{
               createUser.Password = BCryptUtilities.encodePassword(createUser.Password);

               return Ok(userRepository.Create(createUser.ReturnUser()));
            }
        }
        }

        [HttpPost("/api/users/login")]
        public IActionResult Post([FromHeader(Name ="Login")]string email, [FromHeader(Name ="Password")]string password){
            if(userRepository.FindByEmail(email)){
                User inUser = userRepository.GetUserByEmail(email);
                 if(BCryptUtilities.passwordMatch(password, inUser.Password)){
                    UserToken loginUser = userRepository.GetUserTokenByEmail(email, inUser.UserType, inUser.Name, inUser.Surname);
                    Token token = new Token(loginUser.Token);
                    return Ok(token);
                 }
                else{
                    return Unauthorized("Password is not matching!");
                }
                
            }
            else{
                return BadRequest("User with this login doesn't exist in database!");
            }
        }
        [Authorize]
        [HttpDelete("/api/users/{email}")]
        public IActionResult Delete(string email){
            if(userRepository.FindByEmail(email)){
                userRepository.Delete(email);
                return Ok();
            }
            else{
                return BadRequest("User with this login doesn't exist in database!");
            }
        }
        [Authorize]
        [HttpPut("/api/users/{email}")]
        public IActionResult Update([FromBody]User user, string email){
             if(userRepository.FindByEmail(email)){
                User editedUser  = userRepository.GetUserByEmail(email);
                editedUser.Password = BCryptUtilities.encodePassword(editedUser.Password);
                editedUser.Name = user.Name;
                editedUser.Surname = user.Surname;
                return Ok(userRepository.Update(editedUser));
             }
             else{
                return BadRequest("User with this login doesn't exist in database!");
            }
        }
    }
}