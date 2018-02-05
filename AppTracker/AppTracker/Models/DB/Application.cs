using System;
using System.Collections.Generic;

namespace AppTracker.Models.DB
{
    public partial class Application
    {
        public Application()
        {
            ApplicationContactXref = new HashSet<ApplicationContactXref>();
            ApplicationStatus = new HashSet<ApplicationStatus>();
        }

        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? ApplicationDate { get; set; }
        public string Role { get; set; }

        public Company Company { get; set; }
        public ICollection<ApplicationContactXref> ApplicationContactXref { get; set; }
        public ICollection<ApplicationStatus> ApplicationStatus { get; set; }
    }
}
