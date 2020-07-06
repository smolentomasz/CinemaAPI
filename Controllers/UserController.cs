using System.Runtime.InteropServices.WindowsRuntime;
using System.IO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories;

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
        [HttpGet("/api/users")]
        public IActionResult GetList(){
            return Ok(userRepository.GetList());
        }
        //ADD User
        [HttpPost("/api/users/register")]
        public IActionResult Post([FromBody]CreateUserRequest createStop)
        {
        if(createStop.Name.Equals("") || createStop.Surname.Equals("") || createStop.Email.Equals("") || createStop.Password.Equals("")){
            return BadRequest("Missing or invalid data!"); 
        }
        else{
            if(userRepository.FindByEmail(createStop.Email))
                return Conflict("User with this email is existing in database!");
            else{
               createStop.Password = BCryptUtilities.encodePassword(createStop.Password);

               return Ok(userRepository.Create(createStop.ReturnUser()));
            }
        }
        }

        [HttpPost("/api/users/login")]
        public IActionResult Post([FromHeader(Name ="Login")]string email, [FromHeader(Name ="Password")]string password){
            if(userRepository.FindByEmail(email)){
                User loginUser = userRepository.GetUserByEmail(email);
                if(BCryptUtilities.passwordMatch(password, loginUser.Password)){
                    return Ok("Poprawnie zalogowano!");
                }
                else{
                    return Unauthorized();
                }
            }
            else{
                return BadRequest("User with this login doesn't exist in database!");
            }
        }

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