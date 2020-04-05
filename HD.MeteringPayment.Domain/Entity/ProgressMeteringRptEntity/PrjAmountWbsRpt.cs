using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Erp.CommonData.Entity;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity
{
    /// <summary>
    /// 中间计量表头
    /// </summary>
    public partial class PrjAmountWbsRpt : BaseEntity<PrjAmountWbsRpt>
    {
        public PrjAmountWbsRpt() 
        {
            detail = new List<PrjAmountWbsRptDetail>();
        }

        /// <summary>
        /// 中间计量明细
        /// </summary>
        public List<PrjAmountWbsRptDetail> detail
        {
            get;
            set;
        }
    }
}
