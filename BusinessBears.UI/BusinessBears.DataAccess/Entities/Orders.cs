using System;
using System.Collections.Generic;

namespace BusinessBears.DataAccess.Entities
{
    public partial class Orders
    {
        public Orders()
        {
            SoldBears = new HashSet<SoldBears>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public decimal PriceTag { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<SoldBears> SoldBears { get; set; }
    }
}
