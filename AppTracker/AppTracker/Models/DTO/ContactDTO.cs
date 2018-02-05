namespace AppTracker.Models.DTO
{
    public class ContactDTO
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Notes { get; set; }
    }
}
