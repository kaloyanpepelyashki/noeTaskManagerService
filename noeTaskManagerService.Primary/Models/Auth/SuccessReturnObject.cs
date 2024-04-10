namespace noeTaskManagerService.Models.Auth
{
    public class SuccessReturnObject(string firstName, string lastName, string email, string uuid)
    {
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public string Email { get; set; } = email;
        public string UserUID { get; set; } = uuid;
    }
}
