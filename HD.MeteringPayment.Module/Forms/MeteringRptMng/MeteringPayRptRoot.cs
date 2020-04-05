using HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Module.Forms.MeteringRptMng
{
    /// <summary>
    /// 中间计量报表
    /// </summary>
    public class MeteringPayRptRoot
    {
        private List<PrjAmountPayRpt> _allList { get; set; }
        /// <summary>
        /// 所有数据
        /// </summary>
        public List<PrjAmountPayRpt> AllList
        {
            get
            {
                return _allList;
            }
            set
            {
                _allList = value;
                if(value != null)
                {
                    Details = value.FindAll(m => m.Type == 1 && !string.IsNullOrEmpty(m.ItemCode));
                    SumDetail = value.Find(m => m.Type == 1 && String.IsNullOrEmpty(m.ItemCode));
                    if (this.Header != null && !this.Header.IsNewData)
                    {
                        OtherRptLst1 = value.FindAll(m => m.Type == 3);
                        OtherRptLst2 = value.FindAll(m => m.Type == 5);
                        OtherRptLst3 = value.FindAll(m => m.Type == 7);
                    }
                    else
                    {
                        OtherRptLst0 = value.FindAll(m => m.Type == 3);
                        OtherRptLst1 = value.FindAll(m => m.Type == 5);
                        OtherRptLst2 = value.FindAll(m => m.Type == 7);
                        OtherRptLst3 = value.FindAll(m => m.Type == 9);
                    }
                }
            }
        }
        /// <summary>
        /// 报表头
        /// </summary>
        public PrjAmountRpt Header { get; set; }
        /// <summary>
        /// 清单项
        /// </summary>
        public List<PrjAmountPayRpt> Details { get; set; }
        /// <summary>
        /// 清单项小计
        /// </summary>
        public PrjAmountPayRpt SumDetail { get; set; }
        /// <summary>
        /// 合计（奖励\扣款）
        /// </summary>
        public List<PrjAmountPayRpt> OtherRptLst0 { get; set; }
        /// <summary>
        /// 其他计量明细1
        /// </summary>
        public List<PrjAmountPayRpt> OtherRptLst1 { get; set; }
        /// <summary>
        /// 其他计量明细2
        /// </summary>
        public List<PrjAmountPayRpt> OtherRptLst2 { get; set; }

        /// <summary>
        /// 应支付费用
        /// </summary>
        public List<PrjAmountPayRpt> OtherRptLst3 { get; set; }
        

        /// <summary>
        /// 清单支付报表明细3
        /// </summary>
        public List<PrjAmountBoiRpt> BoiDetails{ get; set; }

        /// <summary>
        /// 中间计量支付汇总表明细4
        /// </summary>
        public List<PrjAmountWbsBoiRpt> WbsBoiRptDetails { get; set; }

    }
}
