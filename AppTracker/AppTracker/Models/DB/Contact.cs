using System;
using System.Collections.Generic;

namespace AppTracker.Models.DB
{
    public partial class Contact
    {
        public Contact()
        {
            ApplicationContactXref = new HashSet<ApplicationContactXref>();
        }

        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Notes { get; set; }

        public Company Company { get; set; }
        public ICollection<ApplicationContactXref> ApplicationContactXref { get; set; }
    }
}
