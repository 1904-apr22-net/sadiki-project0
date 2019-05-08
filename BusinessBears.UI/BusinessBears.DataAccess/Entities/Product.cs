using System;
using System.Collections.Generic;

namespace BusinessBears.DataAccess.Entities
{
    public partial class Product
    {
        public Product()
        {
            Inventory = new HashSet<Inventory>();
            SoldTraining = new HashSet<SoldTraining>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? DefPrice { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<SoldTraining> SoldTraining { get; set; }
    }
}
