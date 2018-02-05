using System;
using System.Collections.Generic;

namespace AppTracker.Models.DB
{
    public partial class ApplicationContactXref
    {
        public int Id { get; set; }
        public int? ApplicationId { get; set; }
        public int? ContactId { get; set; }

        public Application Application { get; set; }
        public Contact Contact { get; set; }
    }
}
