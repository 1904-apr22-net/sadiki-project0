using System;
using System.Collections.Generic;

namespace BusinessBears.DataAccess.Entities
{
    public partial class SoldBears
    {
        public SoldBears()
        {
            SoldTraining = new HashSet<SoldTraining>();
        }

        public int BearId { get; set; }
        public int OrderId { get; set; }

        public virtual Orders Order { get; set; }
        public virtual ICollection<SoldTraining> SoldTraining { get; set; }
    }
}
