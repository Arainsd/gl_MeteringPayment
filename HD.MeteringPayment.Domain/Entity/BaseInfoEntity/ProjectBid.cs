using Erp.CommonData.Entity;
using Erp.CommonData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.BaseInfoEntity
{
    public partial class ProjectBid : BaseEntity<ProjectBid>
    {
        public ProjectBid() { }

        public override string ToString()
        {
            return this.BidName;
        }

    }
}
