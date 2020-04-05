using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Erp.CommonData.Entity;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity
{
    /// <summary>
    /// 中间支付证书
    /// </summary>
    public partial class PrjAmountPayRpt : BaseEntity<PrjAmountPayRpt>
    {
        public PrjAmountPayRpt() 
        {
        }

        public string IItemCoe { get; set; }
    }
}
