using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.MeteringPayment.Domain.Entity.ProgressMeteringRptEntity
{
    public partial class PrjAmountPayRpt
    {
        public int Id
        {
            get;
            set;
        }
        public string ProjectNo
        {
            get;
            set;
        }
        public string PrjamountNo
        {
            get;
            set;
        }
        public Nullable<int> Periods
        {
            get;
            set;
        }
        public string PeriodsName
        {
            get;
            set;
        }
        /// <summary>
        /// 1:清单项，2：奖励\扣款，3：奖励\扣款明细 4：其他计量标题1，5：其他计量明细1，6：其他计量标题2，:7：其他计量明细2，9：应支付金额
        /// </summary>
        public int Type 
        { 
            get;
            set; 
        }
        public string ItemNo
        {
            get;
            set;
        }
        public string ItemCode
        {
            get;
            set;
        }
        public string ItemName
        {
            get;
            set;
        }
        public Nullable<decimal> CtrctAmount
        {
            get;
            set;
        }
        public Nullable<decimal> CtrctAmountEx
        {
            get;
            set;
        }
        public Nullable<decimal> ChangeAmount
        {
            get;
            set;
        }
        public Nullable<decimal> ChangeAmountEx
        {
            get;
            set;
        }
        public Nullable<decimal> LatestAmount
        {
            get;
            set;
        }
        public Nullable<decimal> LatestAmountEx
        {
            get;
            set;
        }
        public Nullable<decimal> EndingAmount
        {
            get;
            set;
        }
        public Nullable<decimal> EndingAmountEx
        {
            get;
            set;
        }
        public Nullable<decimal> EndingProp
        {
            get;
            set;
        }
        public Nullable<decimal> StartingAmount
        {
            get;
            set;
        }
        public Nullable<decimal> StartingAmountEx
        {
            get;
            set;
        }
        public Nullable<decimal> LastProp
        {
            get;
            set;
        }
        public Nullable<decimal> Amount
        {
            get;
            set;
        }
        public Nullable<decimal> AmountEx
        {
            get;
            set;
        }
        public Nullable<decimal> Prop
        {
            get;
            set;
        }
        public string Remarks
        {
            get;
            set;
        }
        public int Sequence
        {
            get;
            set;
        }
        public int StatId
        {
            get;
            set;
        }
        public bool RecordValidity
        {
            get;
            set;
        }
        public string CreatedBy
        {
            get;
            set;
        }
        public DateTime CreateDate
        {
            get;
            set;
        }
        public string UpdatedBy
        {
            get;
            set;
        }
        public DateTime RecordDate
        {
            get;
            set;
        }
        public DateTime RowPointer
        {
            get;
            set;
        }
        public bool inWorkflow
        {
            get;
            set;
        }        
    }

}

