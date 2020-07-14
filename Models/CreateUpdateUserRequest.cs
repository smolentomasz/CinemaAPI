namespace Models
{
    public class CreateUpdateUserRequest
    {
        public string Name {get; set;}
        public string Surname {get; set;}
        public string NewPassword {get;set;}
        public string OldPassword {get;set;}

        public CreateUpdateUserRequest(string name, string surname, string newPassword, string oldPassword){
            this.Name = name;
            this.Surname = surname;
            this.NewPassword = newPassword;
            this.OldPassword = oldPassword;
        }
    }
}