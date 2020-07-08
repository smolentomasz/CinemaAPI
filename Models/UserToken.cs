namespace Models
{
    public class UserToken
    {
        public string Name {get;set;}
        public string Surname {get;set;}
        public string Email {get;set;}
        public string UserType {get;set;}
        public string Token {get;set;}

        public UserToken(User user){
            this.Name = user.Name;
            this.Surname = user.Surname;
            this.Email = user.Email;
            this.UserType = user.UserType;
        }
    }
}