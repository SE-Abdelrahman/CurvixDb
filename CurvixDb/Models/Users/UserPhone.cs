namespace CurvixDb.Models.Users
{
    internal class UserPhone
    {
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }

        public User User { get; set; }     // Navigation Property
    }
}
