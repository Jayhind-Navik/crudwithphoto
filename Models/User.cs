namespace Assignment_Ducat.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string PhotoPath { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

}
