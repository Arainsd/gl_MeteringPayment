using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Erp.CommonData.Entity;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity
{
    /// <summary>
    /// 中间计量表汇总表
    /// </summary>
    public partial class PrjAmountWbsBoiRpt : BaseEntity<PrjAmountWbsBoiRpt>
    {
        public PrjAmountWbsBoiRpt() 
        {

        }
        public string IItemCoe { get; set; }
    }
}
