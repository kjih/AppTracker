using System;
using System.Collections.Generic;

namespace AppTracker.Models.DB
{
    public partial class Company
    {
        public Company()
        {
            Application = new HashSet<Application>();
            Contact = new HashSet<Contact>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Notes { get; set; }

        public ICollection<Application> Application { get; set; }
        public ICollection<Contact> Contact { get; set; }
    }
}
