using System;

namespace AppTracker.Models.DTO
{
    public class ApplicationDTO
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string Role { get; set; }
    }
}
