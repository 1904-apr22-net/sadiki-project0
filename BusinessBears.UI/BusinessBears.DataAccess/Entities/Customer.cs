using System;
using System.Collections.Generic;

namespace BusinessBears.DataAccess.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Orders>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? DefLocationId { get; set; }
        public DateTime? LastOrder { get; set; }

        public virtual Location DefLocation { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
