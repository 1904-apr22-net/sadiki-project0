using System;
using System.Collections.Generic;

namespace BusinessBears.DataAccess.Entities
{
    public partial class Location
    {
        public Location()
        {
            Customer = new HashSet<Customer>();
            Inventory = new HashSet<Inventory>();
            Orders = new HashSet<Orders>();
        }

        public int LocationId { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
