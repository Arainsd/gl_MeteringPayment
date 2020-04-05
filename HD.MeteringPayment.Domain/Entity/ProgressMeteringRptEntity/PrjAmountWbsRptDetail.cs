using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Erp.CommonData.Entity;
using Hondee.Common.Attributes;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity
{
    /// <summary>
    /// 中间计量表明细
    /// </summary>
    public partial class PrjAmountWbsRptDetail : BaseEntity<PrjAmountWbsRptDetail>
    {
        public PrjAmountWbsRptDetail() 
        {
        }

        public string IItemCoe { get; set; }
        public string EndStakesNo { get; set; }
        public string StartStakesNo { get; set; }
        public Nullable<decimal> PrjApplyQtyDetail { get; set; }
        public Nullable<decimal> PrjApplyAmountDetail { get; set; }
        public Nullable<decimal> SupervisionQtyDetail { get; set; }
        public Nullable<decimal> SupervisionAmountDetail { get; set; }
        public Nullable<decimal> OwnerQtyDetail { get; set; }
        public Nullable<decimal> OwnerAmountDetail { get; set; }
        [DBField(false)]
        public string StartEndStakesNo
        {
            get
            {
                if (String.IsNullOrEmpty(StartStakesNo) && String.IsNullOrEmpty(EndStakesNo))
                    return "";
                return (StartStakesNo ?? "") + "——" + (EndStakesNo ?? "");
            }
        }
    }
}
