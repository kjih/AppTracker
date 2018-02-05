using System;
using System.Collections.Generic;

namespace AppTracker.Models.DB
{
    public partial class ApplicationStatus
    {
        public int Id { get; set; }
        public int? ApplicationId { get; set; }
        public DateTime? Timestamp { get; set; }
        public bool? Active { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }

        public Application Application { get; set; }
    }
}
