
namespace Models
{
    public class CreateUserRequest
    {
        public string Name {get; set;}
        public string Surname {get; set;}
        public string Email {get;set;}
        public string UserType {get;set;}
        public string Password {get;set;}

        public CreateUserRequest(){

        }
        public User ReturnUser(){
            var user = new User{
                Name = this.Name,
                Surname = this.Surname,
                Email = this.Email,
                UserType = "STANDARD",
                Password = this.Password
            };

            return user;
        }
    }
}