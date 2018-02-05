using System;

namespace AppTracker.Models.DTO
{
    public class ApplicationStatusDTO
    {
        public int Id { get; set; }
        public int? ApplicationId { get; set; }
        public DateTime? Timestamp { get; set; }
        public bool? Active { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}
