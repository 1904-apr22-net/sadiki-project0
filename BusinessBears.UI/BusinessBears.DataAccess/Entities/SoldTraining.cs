using System;
using System.Collections.Generic;

namespace BusinessBears.DataAccess.Entities
{
    public partial class SoldTraining
    {
        public int TrainingId { get; set; }
        public int BearId { get; set; }
        public int ProductId { get; set; }

        public virtual SoldBears Bear { get; set; }
        public virtual Product Product { get; set; }
    }
}
