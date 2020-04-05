using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Erp.CommonData.Entity;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity
{
    /// <summary>
    /// 清单支付报表
    /// </summary>
    public partial class PrjAmountBoiRpt : BaseEntity<PrjAmountBoiRpt>
    {
        public PrjAmountBoiRpt() 
        {
           
        }

        public string IItemCoe { get; set; } 

    }
}
